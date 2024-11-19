using System;
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
                DisplayName = "Accepted Entity Type",
                Type = "input",
                IsRequired = true,
                Name = nameof(OpenCorporatesExternalSearchJobData.AcceptedEntityType),
                Help = "The entity type that defines the golden records you want to enrich. (e.g., /Organization)."
            },
            new()
            {
                DisplayName = "Lookup Vocabulary Key",
                Type = "input",
                IsRequired = true,
                Name = nameof(OpenCorporatesExternalSearchJobData.LookupVocabularyKey),
                Help = "The vocabulary key that contains the names of companies you want to enrich (e.g., organization.name)."
            },
            new()
            {
                DisplayName = "Skip Entity Code Creation (Company Number)",
                Type = "checkbox",
                IsRequired = false,
                Name = nameof(OpenCorporatesExternalSearchJobData.SkipCompanyNumberEntityCodeCreation),
                Help = "Toggle to control the creation of new entity codes using the Company Number."
            }
        };

        public static AuthMethods AuthMethods { get; set; } = new AuthMethods
        {
            Token = new List<Control>() {
                new()
                {
                    DisplayName = "API token",
                    Type = "password",
                    IsRequired = true,
                    Name = nameof(OpenCorporatesExternalSearchJobData.TargetApiKey),
                    Help = "The key to authenticate access to the OpenCorporates API."
                }
            }.Concat(Properties)
        };

        public static Guide Guide { get; set; } = null;
        public static IntegrationType IntegrationType { get; set; } = IntegrationType.Enrichment;
    }
}