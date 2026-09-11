# Migrating a Connector/Enricher to Multi-Version Targeting

This document tracks the migration of `CluedIn.Enricher.OpenCorporates` from a single-version build
to the multi-version targeting pattern. Written as work lands, not after the fact.

Prior art consulted:
- `CluedIn.Connector.Dataverse.V2/docs/multi-version-targeting-migration.md` - the most recent
  migration at the time, itself built on the MasterDataServices/AzureEventHubs/GoogleMaps docs.
- `CluedIn.Enricher.GoogleMaps/docs/multi-version-targeting-migration.md` - directly relevant here:
  this repo hit the **same RestSharp 106↔114 serializer break** GoogleMaps' doc documents, in a
  different shape (a custom `ISerializer`/`IDeserializer`/`IRestSerializer` implementation, not just
  call-site type changes).

Branch: `feature/multi-version-targeting` (off `origin/develop`).

---

## Overview

| CluedIn version | .NET TFM | Package suffix |
|---|---|---|
| 4.7.0 | net6.0 | `.470` |
| 4.8.0 | net6.0 | `.480` |
| 5.0.0-beta.* | net10.0 | `.500` |

**Verified 2026-09-10:** restored a throwaway project against this repo's own `NuGet.config` feeds
with `-p:_CluedIn=5.0.0-*` — resolves to `5.0.0-beta.575`. Beta is current, matching every other
migration done so far on this date.

4.6.0 excluded, matching the MasterDataServices/GoogleMaps precedent - no known 4.6-only dependency
in this enricher's small API surface.

No real test project exists (`test/acceptance` is a PowerShell script, not a .NET test project;
`test/Directory.Build.props` unconditionally references xunit.v3/AutoFixture.Xunit3 but nothing
under `test/` consumes it - same "dead scaffold" situation GoogleMaps' doc found and left alone
since fixing it isn't necessary for a working build).

---

## Step 1 — Pipeline (`azure-pipelines.yml`)

Status: **Done**

Switched from `crawler.build.yml` (`windows-latest` pool) to `crawler.build.jobs.yml` with
`multiVersionCluedInTargets` (4.7.0/4.8.0/5.0.0-beta.*), matching the `ubuntu-22.04` pool every
other migrated repo uses. Dropped the explicit `NuGetAuthenticate@0` step (each job in the
jobs-template does its own).

## Step 2 — `Directory.Build.props`

Status: **Done**

Honours `CluedInMultiVersionTargetFramework` (net10.0 local fallback), derives
`CLUEDIN_V47`/`V48`/`V50` `DefineConstants`. **Same `LangVersion` trap GoogleMaps' doc
describes:** `Constants.cs` uses a raw string literal (`$$"""..."""`, C# 11+), and net6.0's own
default LangVersion is C# 10 - pinned `LangVersion` to `13.0` up front to avoid `CS8936`.

## Step 3 — `Packages.props`

Status: **Done**

`_CluedIn` guarded so the pipeline's per-leg override wins. No TFM-conditional package overrides
needed.

## Step 4 — `NuGet.config`

Status: **Done**

Renamed `Nuget.config` → `NuGet.config` (two-step `git mv`, Windows is case-insensitive). Verified:
no extra feed needed - `CluedIn.Core` restores cleanly at `4.7.0`/`4.8.0` against this repo's
existing `develop`/`release`/`AzurePipelines`/`nuget.org` feeds (no `public` feed present, unlike
AzureEventHubs' repo, and it wasn't needed here either).

## Step 5 — API compatibility audit across 4.7.0 / 4.8.0 / 5.0.0-beta.*

Status: **Done**

### Finding: RestSharp 106↔114 serializer interface break (same family as GoogleMaps' finding, different shape)

Resolved RestSharp versions per leg (confirmed via `project.assets.json`, not assumed):
- 4.7.0/4.8.0 (net6.0) → **RestSharp 106.15.0**
- 5.0.0-beta.* (net10.0) → **RestSharp 114.0.0**

Unlike GoogleMaps' repo (only individual call-site type/enum changes), this repo has a **custom
`NewtonsoftJsonSerializer`** (`src/ExternalSearch.Providers.OpenCorporates/NewtonsoftJsonSerializer.cs`)
implementing RestSharp's serializer interfaces directly - and nearly every member signature differs
between the two RestSharp generations:

| Member | RestSharp 106.15.0 (old) | RestSharp 114.0.0 (new) |
|---|---|---|
| `ISerializer.ContentType` | `string` | `RestSharp.ContentType` (struct) |
| `IDeserializer` namespace | `RestSharp.Deserializers.IDeserializer` | `RestSharp.Serializers.IDeserializer` |
| `IDeserializer.Deserialize<T>` param | `IRestResponse response` | `RestResponse response` |
| `IRestSerializer` namespace | `RestSharp.Serialization.IRestSerializer` | `RestSharp.Serializers.IRestSerializer` |
| `IRestSerializer` members | `SupportedContentTypes`, `DataFormat`, `Serialize(Parameter)` | `Serializer`, `Deserializer`, `AcceptedContentTypes`, `SupportsContentType` (delegate) |

Confirmed via reflection against the actual restored DLLs (`System.Reflection.Assembly.LoadFrom`
against the NuGet-cached `RestSharp.dll` for each version) rather than guessing from memory or
docs - this diverged enough from GoogleMaps' finding that guessing would have been wrong.

Fixed with a full `#if CLUEDIN_V50 ... #else ... #endif` split of the entire class (not
member-by-member `#if`s - too many members differ). Also guarded, in
`OpenCorporatesExternalSearchProvider.cs`:
- `RestClient` construction (2 call sites) - `RestClientOptions`-based constructor only exists in
  RestSharp 107+; old code uses `new RestClient(baseUrl).UseSerializer(...)`.
- `Method.Get` → `Method.GET` (4 call sites - same enum-casing break GoogleMaps documented).
- `ConstructVerifyConnectionResponse`'s parameter type: `RestResponse` (new) vs `IRestResponse`
  (old) - the non-generic base interface, since the method only touches `StatusCode`,
  `StatusDescription`, `ErrorException`, `Content`, all present on both.

`var`-inferred response locals (`response`, `searchCompanyResponse`, `searchDetailResponse`) needed
no guard, matching GoogleMaps' finding.

Verified: all three legs (4.7.0/net6.0, 4.8.0/net6.0, 5.0.0-beta.*/net10.0) build 0 errors, plus the
local-dev default (net10.0/5.0.0-*).

## Step 6 — Reset the semantic version (`GitVersion.yml`)

Status: **Done**

```yaml
next-version: 1.0
ignore:
  sha: []
  commits-before: 2026-04-01T00:00:00
```

Highest pre-existing tag: `4.6.1`/`v4.6.1` at `2026-03-18T15:03:45Z`.

**Lesson for the next repo in this batch:** a cutoff too close to the tag's own timestamp doesn't
reliably work. First tried `2026-03-19T00:00:00` (9h after the tag) - verified with the pipeline's
actual pinned `GitVersion.Tool 5.9.0` (installed to a scratch tool-path) and it **did not** exclude
the tag; `MajorMinorPatch` still resolved to `4.7.0`. Widening the cutoff to `2026-04-01T00:00:00`
(two weeks later) fixed it - `MajorMinorPatch` resolved to `1.0.0` as expected. Suspect a timezone
interpretation quirk in GitVersion 5.9's `commits-before` (the tag's timestamp has an explicit
`Z`/offset; the cutoff in this config doesn't), but didn't fully root-cause it - just use a
generous multi-day margin from now on rather than a same-day cutoff, and always verify with the
real pinned tool rather than trusting the config alone.

## Step 7 — Push and confirm CI

Status: **Done**

PR #39, build 151865 — fully green on the first push: all three `Multi-version build+test` legs
(4.7.0, 4.8.0, 5.0.0-beta.*) and `Multi-version: publish` passed.

**Cross-repo warning received mid-migration (from the coordinator, relayed from the
`CluedIn.Enricher.Gleif` migration running in the same batch):** `GitVersion.Tool 5.9.0` appears to
parse `ignore.commits-before` using local machine time, not UTC, and fails **silently** - it just
keeps incrementing off the old highest tag instead of resetting to `1.0.0`, with no error and a
green CI build (a wrong version number doesn't fail the build, it just publishes under the wrong
one). This matches what Step 6 above already found independently for this repo (a 9-hour-after-tag
cutoff silently failed; only a ~2-week margin worked). Re-verified after the CI run, not just
during Step 6: ran the pinned tool again and confirmed `MajorMinorPatch` is still `1.0.0` /
`SemVer` is `1.0.0-multi-version-targeting.93`. **Recommendation for the remaining repos in this
batch:** always pad `commits-before` by at least 2 full days past the highest tag's actual commit
date, and always explicitly check the resolved `MajorMinorPatch` (not just that the config file
"looks right" or that CI is green) before trusting it.

---

## Checklist

- [x] `azure-pipelines.yml` — switched to `crawler.build.jobs.yml` with `multiVersionCluedInTargets` (4.7.0, 4.8.0, 5.0.0-beta.*)
- [x] `Directory.Build.props` — honours `CluedInMultiVersionTargetFramework` with net10.0 local fallback; `DefineConstants` derived; `LangVersion` pinned to 13.0
- [x] `Packages.props` — `_CluedIn` guarded
- [x] `NuGet.config` — renamed from `Nuget.config`; verified sufficient as-is, no extra feed needed
- [x] Source — RestSharp 106↔114 serializer break fixed (`NewtonsoftJsonSerializer.cs` full `#if` split; `RestClient` construction, `Method.Get`/`GET`, and `ConstructVerifyConnectionResponse` parameter type guarded in `OpenCorporatesExternalSearchProvider.cs`); all three legs build 0 errors
- [x] `GitVersion.yml` — `next-version: 1.0`; `ignore.commits-before: 2026-04-01T00:00:00` (widened after a too-tight cutoff failed); verified with the pipeline's actual pinned GitVersion.Tool 5.9.0, both before and after the CI run
- [x] Pushed branch and confirmed the Azure DevOps pipeline is green end-to-end — PR #39, build 151865: all three legs + `Multi-version: publish` passed
