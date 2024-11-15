﻿using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Core.Data.Relational;
using CluedIn.Core.Providers;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates
{
    public static class Constants
    {
        public const string ComponentName = "OpenCorporates";
        public const string ProviderName = "Open Corporates";
        public static readonly Guid ProviderId = Core.Constants.ExternalSearchProviders.OpenCorporatesId;


        public static string About { get; set; } = "Open Corporates is an enricher which provides information on all companies worldwide";
        public static string Icon { get; set; } = "Resources.opencorporates.svg";
        public static string Domain { get; set; } = "https://opencorporates.com/";

        public static IEnumerable<Control> Properties { get; set; } = new List<Control>()
        {
            new()
            {
                displayName = "Accepted Entity Type",
                type = "input",
                isRequired = true,
                name = nameof(OpenCorporatesExternalSearchJobData.AcceptedEntityType)
            },
            new()
            {
                displayName = "Lookup Vocabulary Key",
                type = "input",
                isRequired = true,
                name = nameof(OpenCorporatesExternalSearchJobData.LookupVocabularyKey)
            },
            new()
            {
                displayName = "Skip Entity Code Creation (Company Number)",
                type = "checkbox",
                isRequired = false,
                name = nameof(OpenCorporatesExternalSearchJobData.SkipCompanyNumberEntityCodeCreation)
            }
        };

        public static AuthMethods AuthMethods { get; set; } = new AuthMethods
        {
            token = new List<Control>() {
                new()
                {
                    displayName = "API token",
                    type = "password",
                    isRequired = true,
                    name = nameof(OpenCorporatesExternalSearchJobData.TargetApiKey)
                }
            }.Concat(Properties)
        };

        public static Guide Guide { get; set; } = null;
        public static IntegrationType IntegrationType { get; set; } = IntegrationType.Enrichment;
    }
}