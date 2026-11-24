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
        public static readonly string Instruction = $$"""
            [
              {
                "type": "bulleted-list",
                "children": [
                  {
                    "type": "list-item",
                    "children": [
                      {
                        "text": "Add the {{Core.Constants.DomainLabels.EntityType.ToLower()}} to specify the golden records you want to enrich. Only golden records belonging to that {{Core.Constants.DomainLabels.EntityType.ToLower()}} will be enriched."
                      }
                    ]
                  },
                  {
                    "type": "list-item",
                    "children": [
                      {
                        "text": "Add the vocabulary keys to provide the input for the enricher to search for additional information. For example, if you provide the website vocabulary key for the Web enricher, it will use specific websites to look for information about companies. In some cases, vocabulary keys are not required. If you don't add them, the enricher will use default vocabulary keys."
                      }
                    ]
                  },
                  {
                    "type": "list-item",
                    "children": [
                      {
                        "text": "Add the API key to enable the enricher to retrieve information from a specific API. For example, the Vatlayer enricher requires an access key to authenticate with the Vatlayer API."
                      }
                    ]
                  }
                ]
              }
            ]
            """;

        public struct KeyName
        {
            public const string TargetApiKey = "targetApiKey";
            public const string AcceptedEntityType = "acceptedEntityType";
            public const string LookupVocabularyKey = "lookupVocabularyKey";
        }

        public static string About { get; set; } = "Open Corporates is an enricher which provides information on all companies worldwide";
        public static string Icon { get; set; } = "Resources.opencorporates.svg";
        public static string Domain { get; set; } = "https://opencorporates.com/";

        private static Dictionary<string, object> CreateFieldGroup(string id, string label, params string[] fields) => new()
        {
            ["id"] = id,
            ["label"] = label,
            ["fields"] = fields,
        };

        /// <summary>
        /// The sections the UI groups the configuration fields into, in order. Fields no group lists
        /// (such as the filter and rate limit settings added by CluedIn) go to the last group.
        /// </summary>
        public static readonly Dictionary<string, object>[] FieldGroups =
        [
            CreateFieldGroup("connection", "Connection", KeyName.TargetApiKey),
            CreateFieldGroup("whatToEnrich", "What to enrich", KeyName.AcceptedEntityType, "enricherRuleCondition"),
            CreateFieldGroup("fieldMapping", "Field mapping", KeyName.LookupVocabularyKey),
            CreateFieldGroup("advanced", "Advanced"),
        ];

        public static IEnumerable<Control> Properties { get; set; } = new List<Control>()
        {
            new()
            {
                DisplayName = $"Accepted {Core.Constants.DomainLabels.EntityType}",
                Type = "entityTypeSelector",
                IsRequired = true,
                Name = KeyName.AcceptedEntityType,
                Help = $"The {Core.Constants.DomainLabels.EntityType.ToLower()} that defines the golden records you want to enrich. (e.g., /Organization)."
            },
            new()
            {
                DisplayName = "Lookup Vocabulary Key",
                Type = "vocabularyKeySelector",
                IsRequired = true,
                Name = KeyName.LookupVocabularyKey,
                Help = "The vocabulary key that contains the names of companies you want to enrich (e.g., organization.name)."
            },
        };

        public static AuthMethods AuthMethods { get; set; } = new AuthMethods
        {
            Token = new List<Control>() {
                new()
                {
                    DisplayName = "API Key",
                    Type = "password",
                    IsRequired = true,
                    Name = KeyName.TargetApiKey,
                    Help = "The key to authenticate access to the OpenCorporates API.",
                    ValidationRules = new List<Dictionary<string, string>>()
                    {
                        new() {
                            { "regex", "\\s" },
                            { "message", "Spaces are not allowed" }
                        }
                    },
                }
            }.Concat(Properties)
        };

        public static Guide Guide { get; set; } = new Guide
        {
            Instructions = Instruction
        };
        public static IntegrationType IntegrationType { get; set; } = IntegrationType.Enrichment;
    }
}