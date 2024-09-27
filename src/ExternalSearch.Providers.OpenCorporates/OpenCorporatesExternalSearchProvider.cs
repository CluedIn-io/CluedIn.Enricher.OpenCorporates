// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OpenCorporatesExternalSearchProvider.cs" company="Clued In">
//   Copyright (c) 2019 Clued In. All rights reserved.
// </copyright>
// <summary>
//   Implements the open corporates external search provider class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Data.Parts;
using CluedIn.Core.Data.Relational;
using CluedIn.Core.ExternalSearch;
using CluedIn.Core.Providers;
using RestSharp;
using CluedIn.ExternalSearch.Providers.OpenCorporates.Model;
using CluedIn.ExternalSearch.Providers.OpenCorporates.Vocabularies;
using CluedIn.Crawling.Helpers;
using Microsoft.Extensions.Logging;
using EntityType = CluedIn.Core.Data.EntityType;
using CluedIn.ExternalSearch.Provider;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates
{
    public class OpenCorporatesExternalSearchProvider : ExternalSearchProviderBase, IExtendedEnricherMetadata, IConfigurableExternalSearchProvider
    {
        /**********************************************************************************************************
         * FIELDS
         **********************************************************************************************************/

        private static readonly EntityType[] DefaultAcceptedEntityTypes = { EntityType.Organization };

        /**********************************************************************************************************
         * CONSTRUCTORS
         **********************************************************************************************************/

        public OpenCorporatesExternalSearchProvider()
            : base(Constants.ProviderId, DefaultAcceptedEntityTypes)
        {
        }

        /**********************************************************************************************************
         * METHODS
         **********************************************************************************************************/

        public IEnumerable<EntityType> Accepts(IDictionary<string, object> config, IProvider provider) => this.Accepts(config);

        private IEnumerable<EntityType> Accepts(IDictionary<string, object> config)
        {
            if (config != null)
            {
                var openCorporatesExternalSearchJobData = new OpenCorporatesExternalSearchJobData(config);
                if (!string.IsNullOrWhiteSpace(openCorporatesExternalSearchJobData.AcceptedEntityType))
                    return new EntityType[] { openCorporatesExternalSearchJobData.AcceptedEntityType };
            }
            // Fallback to default accepted entity types
            return DefaultAcceptedEntityTypes;
        }

        private bool Accepts(IDictionary<string, object> config, EntityType entityTypeToEvaluate)
        {
            var configurableAcceptedEntityTypes = this.Accepts(config).ToArray();

            return configurableAcceptedEntityTypes.Any(entityTypeToEvaluate.Is);
        }

        public IEnumerable<IExternalSearchQuery> BuildQueries(ExecutionContext context, IExternalSearchRequest request, IDictionary<string, object> config, IProvider provider)
        {
            if (!this.Accepts(config, request.EntityMetaData.EntityType))
                yield break;

            var existingResults = request.GetQueryResults<Company>(this).ToList();

            bool NameFilter(string value)
            {
                return existingResults.Any(r => string.Equals(r.Data.name, value, StringComparison.InvariantCultureIgnoreCase));
            }

            var entityType = request.EntityMetaData.EntityType;
            var organizationName = request.QueryParameters.GetValue(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.OrganizationName, new HashSet<string>());

            var openCorporatesExternalSearchJobData = new OpenCorporatesExternalSearchJobData(config);
            if (!string.IsNullOrWhiteSpace(openCorporatesExternalSearchJobData.LookupVocabularyKey))
                organizationName = request.QueryParameters.GetValue<string, HashSet<string>>(openCorporatesExternalSearchJobData.LookupVocabularyKey, new HashSet<string>());

            var companyCodes = OpenCorporatesUtil.GetAllCodesFromRequest(request);

            if (companyCodes != null && companyCodes.Count > 0)
            {
                // Note: Do not filter codes here - If the initial search query only contained a name, in the next iteration the request will contain ids to look up from previous results.
                foreach (var companyCode in companyCodes)
                {
                    var companyCodeLookupQuery = new Dictionary<string, string>
                    {
                        { "jurisdiction", companyCode.Key },
                        { ExternalSearchQueryParameter.Identifier.ToString(), companyCode.Value }
                    };

                    yield return new ExternalSearchQuery(this, entityType, companyCodeLookupQuery);
                }
            }
            else if (organizationName != null && organizationName.Count > 0)
            {
                foreach (var value in organizationName.Where(v => !NameFilter(v)))
                    yield return new ExternalSearchQuery(this, entityType, ExternalSearchQueryParameter.Name, value);
            }
        }

        public IEnumerable<IExternalSearchQueryResult> ExecuteSearch(ExecutionContext context, IExternalSearchQuery query, IDictionary<string, object> config, IProvider provider)
        {
            var openCorporatesExternalSearchJobData = new OpenCorporatesExternalSearchJobData(config);

            var nameLookup = query.QueryParameters.ContainsKey(ExternalSearchQueryParameter.Name) ? query.QueryParameters[ExternalSearchQueryParameter.Name].FirstOrDefault() : null;

            var jurisdictionCodeLookup = new 
            {
                Jurisdiction = query.QueryParameters.ContainsKey("jurisdiction") ? query.QueryParameters["jurisdiction"].FirstOrDefault() : null,
                Value        = query.QueryParameters.ContainsKey(ExternalSearchQueryParameter.Identifier.ToString()) ? query.QueryParameters[ExternalSearchQueryParameter.Identifier.ToString()].FirstOrDefault() : null
            };

            if (nameLookup == null && jurisdictionCodeLookup.Value == null)
                yield break;

            var client = new RestClient("https://api.opencorporates.com/v0.4");
            client.AddHandler(() => NewtonsoftJsonSerializer.Default,"application/json");

            var request = !string.IsNullOrEmpty(nameLookup)
                ? new RestRequest($"/companies/search?q={nameLookup}", Method.GET) // This will return a sparse company result
                : new RestRequest($"companies/{jurisdictionCodeLookup.Jurisdiction}/{jurisdictionCodeLookup.Value}?format=json", Method.GET);

            request.AddQueryParameter("api_token", openCorporatesExternalSearchJobData.TargetApiKey);

            var response = client.ExecuteAsync<OpenCorporatesResponse>(request).Result;

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                {
                    if (response.Data != null)
                    {
                        List<FullCompanyObject> results;

                        if (response.Data.results.company != null)
                        {
                            var fullOpenCorporateObject = new FullCompanyObject { Company = response.Data.results.company };

                            results = new List<FullCompanyObject>() { fullOpenCorporateObject };
                        }
                        else
                        {
                            results = response.Data.results.companies.Select(c => new FullCompanyObject { Company = c.company }).ToList();
                        }

                        foreach (var fullOpenCorporateObject in results)
                        {
                            yield return new ExternalSearchQueryResult<FullCompanyObject>(query, fullOpenCorporateObject);
                        }
                    }

                    break;
                }
                case HttpStatusCode.NoContent:
                case HttpStatusCode.NotFound:
                    yield break;
                default:
                {
                    if (response.ErrorException != null)
                        throw new AggregateException(response.ErrorException.Message, response.ErrorException);
                    else if (response.Content != null)
                        throw new ApplicationException("Could not execute external search query - StatusCode:" + response.StatusCode + " - " + response.Content);
                    else
                        throw new ApplicationException("Could not execute external search query - StatusCode:" + response.StatusCode);
                }
            }
        }

        public IEnumerable<Clue> BuildClues(ExecutionContext context, IExternalSearchQuery query, IExternalSearchQueryResult result, IExternalSearchRequest request, IDictionary<string, object> config, IProvider provider)
        {
            var resultItem = result.As<FullCompanyObject>();

            var code = new EntityCode(EntityType.Organization, CodeOrigin.CluedIn.CreateSpecific("openCorporates"), resultItem.Data.Company.company_number);

            var clue = new Clue(code, context.Organization);

            clue.Data.OriginProviderDefinitionId = this.Id;

            this.PopulateMetadata(context, clue.Data.EntityData, resultItem, request);

            yield return clue;

            if (resultItem.Data.Company.filings != null)
            {
                foreach (var filing in resultItem.Data.Company.filings.Select(f => f.filing))
                {
                    var filingCode = new EntityCode(EntityType.Activity, CodeOrigin.CluedIn.CreateSpecific("openCorporates"), filing.id);

                    var filingClue = new Clue(filingCode, context.Organization);

                    filingClue.Data.OriginProviderDefinitionId = this.Id;

                    this.PopulateMetadata(filingClue.Data.EntityData, filing, clue);

                    yield return filingClue;
                }
            }
        }

        public IEntityMetadata GetPrimaryEntityMetadata(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request, IDictionary<string, object> config, IProvider provider)
        {
            var resultItem = result.As<FullCompanyObject>();
            return this.CreateMetadata(context, resultItem, request);
        }

        public override IPreviewImage GetPrimaryEntityPreviewImage(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            // Note: This needs to be cleaned up, but since config and provider is not used in GetPrimaryEntityMetadata this is fine.
            var dummyConfig   = new Dictionary<string, object>();
            var dummyProvider = new DefaultExternalSearchProviderProvider(context.ApplicationContext, this);

            return GetPrimaryEntityPreviewImage(context, result, request, dummyConfig, dummyProvider);
        }

        public IPreviewImage GetPrimaryEntityPreviewImage(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request, IDictionary<string, object> config, IProvider provider)
        {
            return null;
        }

        private void PopulateMetadata(IEntityMetadataPart metadata, Filing filing, Clue companyClue)
        {
            var code = new EntityCode(EntityType.Activity, CodeOrigin.CluedIn.CreateSpecific("openCorporates"), filing.id);

            metadata.EntityType = EntityType.Activity;

            metadata.Name           = filing.title;
            metadata.Description    = filing.description;
            metadata.ModifiedDate   = filing.date;

            if (!string.IsNullOrEmpty(filing.opencorporates_url))
                metadata.Uri = new Uri(filing.opencorporates_url);

            metadata.Properties[OpenCorporatesVocabulary.Filing.Date]              = filing.date.PrintIfAvailable();
            metadata.Properties[OpenCorporatesVocabulary.Filing.FilingCode]        = filing.filing_type_code.PrintIfAvailable();
            metadata.Properties[OpenCorporatesVocabulary.Filing.FilingType]        = filing.filing_type_name.PrintIfAvailable();
            metadata.Properties[OpenCorporatesVocabulary.Filing.OpenCorporatesUrl] = filing.opencorporates_url.PrintIfAvailable();
            metadata.Properties[OpenCorporatesVocabulary.Filing.Uid]               = filing.uid.PrintIfAvailable();

            var filingUrl = filing.url?.ToString();
            if (!string.IsNullOrWhiteSpace(filingUrl) && Uri.TryCreate(filing.url.ToString(), UriKind.Absolute, out var uri))
                metadata.Uri = uri;

            metadata.Codes.Add(code);
            metadata.OriginEntityCode = code;

            metadata.OutgoingEdges.Add(new EntityEdge(EntityReference.CreateByKnownCode(code), EntityReference.CreateByKnownCode(companyClue.OriginEntityCode, companyClue.Data.EntityData.Name), EntityEdgeType.Parent));
        }

        private IEntityMetadata CreateMetadata(ExecutionContext context, IExternalSearchQueryResult<FullCompanyObject> resultItem, IExternalSearchRequest request)
        {
            var metadata = new EntityMetadataPart();

            this.PopulateMetadata(context, metadata, resultItem, request);

            return metadata;
        }

        private void PopulateMetadata(ExecutionContext context, IEntityMetadata metadata, IExternalSearchQueryResult<FullCompanyObject> resultItem, IExternalSearchRequest request)
        {
            var code = new EntityCode(EntityType.Organization, CodeOrigin.CluedIn.CreateSpecific("openCorporates"), resultItem.Data.Company.company_number);

            metadata.EntityType = request.EntityMetaData.EntityType;

            metadata.Name           = request.EntityMetaData.Name;
            metadata.CreatedDate    = resultItem.Data.Company.created_at;
            metadata.ModifiedDate   = resultItem.Data.Company.updated_at;

            metadata.Codes.Add(request.EntityMetaData.OriginEntityCode);

            metadata.Aliases.AddRange(resultItem.Data.Company.alternative_names?.Where(a => !string.IsNullOrEmpty(a.company_name)).Select(p => p.company_name) ?? new string[0]);
            metadata.Aliases.AddRange(resultItem.Data.Company.previous_names?.Where(a => !string.IsNullOrEmpty(a.company_name)).Select(p => p.company_name) ?? new string[0]);

            metadata.Properties[OpenCorporatesVocabulary.Organization.AgentAddress]             = resultItem.Data.Company.agent_address.PrintIfAvailable();
            metadata.Properties[OpenCorporatesVocabulary.Organization.AgentName]                = resultItem.Data.Company.agent_name.PrintIfAvailable();
            metadata.Properties[OpenCorporatesVocabulary.Organization.AlternativeNames]         = resultItem.Data.Company.alternative_names.PrintIfAvailable(v => string.Join(", ", v.Where(a => !string.IsNullOrEmpty(a.company_name)).Select(a => a.company_name)));
            metadata.Properties[OpenCorporatesVocabulary.Organization.Branch]                   = resultItem.Data.Company.branch.PrintIfAvailable(); 
            metadata.Properties[OpenCorporatesVocabulary.Organization.BranchStatus]             = resultItem.Data.Company.branch_status.PrintIfAvailable(); 
            metadata.Properties[OpenCorporatesVocabulary.Organization.CompanyNumber]            = resultItem.Data.Company.company_number.PrintIfAvailable();
            metadata.Properties[OpenCorporatesVocabulary.Organization.CompanyType]              = resultItem.Data.Company.company_type.PrintIfAvailable();
            metadata.Properties[OpenCorporatesVocabulary.Organization.CorporateGroupings]       = resultItem.Data.Company.corporate_groupings.PrintIfAvailable(JsonUtility.Serialize); 
            metadata.Properties[OpenCorporatesVocabulary.Organization.CreatedAt]                = resultItem.Data.Company.created_at.PrintIfAvailable();
            metadata.Properties[OpenCorporatesVocabulary.Organization.CurrentStatus]            = resultItem.Data.Company.current_status.PrintIfAvailable();
            metadata.Properties[OpenCorporatesVocabulary.Organization.Data]                     = resultItem.Data.Company.data.PrintIfAvailable(JsonUtility.Serialize);
            metadata.Properties[OpenCorporatesVocabulary.Organization.DissolutionDate]          = resultItem.Data.Company.dissolution_date.PrintIfAvailable();
            metadata.Properties[OpenCorporatesVocabulary.Organization.Filings]                  = resultItem.Data.Company.filings.PrintIfAvailable(JsonUtility.Serialize);
            metadata.Properties[OpenCorporatesVocabulary.Organization.HomeCompany]              = resultItem.Data.Company.home_company.PrintIfAvailable(JsonUtility.Serialize);
            metadata.Properties[OpenCorporatesVocabulary.Organization.Identifiers]              = resultItem.Data.Company.identifiers.PrintIfAvailable(JsonUtility.Serialize);
            metadata.Properties[OpenCorporatesVocabulary.Organization.InActive]                 = resultItem.Data.Company.inactive.PrintIfAvailable();
            metadata.Properties[OpenCorporatesVocabulary.Organization.IncorporationDate]        = resultItem.Data.Company.incorporation_date.PrintIfAvailable();
            metadata.Properties[OpenCorporatesVocabulary.Organization.IndustryCodes]            = resultItem.Data.Company.industry_codes.PrintIfAvailable(v => string.Join(", ", v.Where(i => !string.IsNullOrEmpty(i.IndustryCode.code)).Select(i => i.IndustryCode.code)));
            metadata.Properties[OpenCorporatesVocabulary.Organization.JurisdictionCode]         = resultItem.Data.Company.jurisdiction_code.PrintIfAvailable();
            metadata.Properties[OpenCorporatesVocabulary.Organization.AddressCountryCode]       = resultItem.Data.Company.jurisdiction_code != null && resultItem.Data.Company.jurisdiction_code.Length == 2 ? resultItem.Data.Company.jurisdiction_code : null;
            metadata.Properties[OpenCorporatesVocabulary.Organization.NativeCompanyNumber]      = resultItem.Data.Company.native_company_number.PrintIfAvailable();
            metadata.Properties[OpenCorporatesVocabulary.Organization.NumberOfEmployees]        = resultItem.Data.Company.number_of_employees.PrintIfAvailable();
            metadata.Properties[OpenCorporatesVocabulary.Organization.Officers]                 = resultItem.Data.Company.officers.PrintIfAvailable(v => string.Join(", ", v.Where(c => !string.IsNullOrEmpty(c.Officer.name)).Select(c => c.Officer.name)));
            metadata.Properties[OpenCorporatesVocabulary.Organization.OfficersJson]             = resultItem.Data.Company.officers.PrintIfAvailable(JsonUtility.Serialize);
            metadata.Properties[OpenCorporatesVocabulary.Organization.OpenCorporatesUrl]        = resultItem.Data.Company.opencorporates_url.PrintIfAvailable();
            metadata.Properties[OpenCorporatesVocabulary.Organization.PreviousNames]            = resultItem.Data.Company.previous_names.PrintIfAvailable(v => string.Join(", ", v.Where(p => string.IsNullOrEmpty(p.company_name)).Select(p => p.company_name)));
            metadata.Properties[OpenCorporatesVocabulary.Organization.RegisteredAddress]        = resultItem.Data.Company.registered_address_in_full.PrintIfAvailable(); 
            metadata.Properties[OpenCorporatesVocabulary.Organization.RegistryUrl]              = resultItem.Data.Company.registry_url.PrintIfAvailable();
            metadata.Properties[OpenCorporatesVocabulary.Organization.SourcePublisher]          = resultItem.Data.Company.source?.publisher.PrintIfAvailable();
            metadata.Properties[OpenCorporatesVocabulary.Organization.SourceUrl]                = resultItem.Data.Company.source?.publisher.PrintIfAvailable();
            metadata.Properties[OpenCorporatesVocabulary.Organization.TrademarkRegistration]    = resultItem.Data.Company.trademark_registrations.PrintIfAvailable(JsonUtility.Serialize);
            metadata.Properties[OpenCorporatesVocabulary.Organization.UpdatedAt]                = resultItem.Data.Company.updated_at.PrintIfAvailable();

            // Looks like this the user input via the profile page
            if (resultItem.Data.Company.data?.most_recent != null)
            {
                var dataByType = resultItem.Data.Company.data?.most_recent.GroupBy(d => d.datum.data_type);

                foreach (var data in dataByType)
                {
                    var groupedValues   = data.GroupBy(v => v.datum.description).OrderByDescending(g => g.Count()).ThenByDescending(g => g.Max(v => v.datum.id));
                    var bestValue       = groupedValues.FirstOrDefault()?.FirstOrDefault()?.datum.description;

                    switch (data.Key.ToLowerInvariant())
                    {
                        case "website":
                            metadata.Properties[OpenCorporatesVocabulary.Organization.Website] = bestValue.PrintIfAvailable();
                            break;

                        case "telephonenumber":
                            metadata.Properties[OpenCorporatesVocabulary.Organization.TelephoneNumber] = bestValue.PrintIfAvailable();
                            break;

                        case "companyaddress":
                            // registered_address_in_full already contains the address.
                            break;

                        case "officialregisterentry":
                            metadata.Properties[OpenCorporatesVocabulary.Organization.OfficialRegisterEntry] = bestValue.PrintIfAvailable();
                            break;

                        default:
                            context.Log.LogWarning("Unknown OpenCorporates data type: Key: {Key} - Data: {@Data}", data.Key, new { data });
                            break;
                    }
                }
            }

            metadata.Codes.Add(code);
            metadata.OriginEntityCode = code;
        }


        // Since this is a configurable external search provider, theses methods should never be called
        public override IEnumerable<IExternalSearchQuery> BuildQueries(ExecutionContext context, IExternalSearchRequest request) => throw new NotSupportedException();
        public override bool Accepts(EntityType entityType) => throw new NotSupportedException();
        public override IEnumerable<IExternalSearchQueryResult> ExecuteSearch(ExecutionContext context, IExternalSearchQuery query) => throw new NotSupportedException();
        public override IEnumerable<Clue> BuildClues(ExecutionContext context, IExternalSearchQuery query, IExternalSearchQueryResult result, IExternalSearchRequest request) => throw new NotSupportedException();
        public override IEntityMetadata GetPrimaryEntityMetadata(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request) => throw new NotSupportedException();

        /**********************************************************************************************************
         * PROPERTIES
         **********************************************************************************************************/

        public string Icon { get; } = Constants.Icon;
        public string Domain { get; } = Constants.Domain;
        public string About { get; } = Constants.About;

        public AuthMethods AuthMethods { get; } = Constants.AuthMethods;
        public IEnumerable<Control> Properties { get; } = Constants.Properties;
        public Guide Guide { get; } = Constants.Guide;
        public IntegrationType Type { get; } = Constants.IntegrationType;
    }
}