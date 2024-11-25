using System.Collections.Generic;
using CluedIn.Core.Crawling;
using static CluedIn.ExternalSearch.Providers.OpenCorporates.Constants;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates
{
    public class OpenCorporatesExternalSearchJobData : CrawlJobData
    {
        public OpenCorporatesExternalSearchJobData(IDictionary<string, object> configuration)
        {
            TargetApiKey = GetValue<string>(configuration, nameof(TargetApiKey), default(string));
            AcceptedEntityType = GetValue(configuration, KeyName.AcceptedEntityType, default(string));
            LookupVocabularyKey = GetValue<string>(configuration, KeyName.LookupVocabularyKey, default(string));
            SkipCompanyNumberEntityCodeCreation = GetValue(configuration, KeyName.SkipCompanyNumberEntityCodeCreation, default(bool));
        }

        public IDictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>()
            {
                { nameof(TargetApiKey), TargetApiKey },
                { KeyName.AcceptedEntityType, AcceptedEntityType },
                { KeyName.LookupVocabularyKey, LookupVocabularyKey },
                { KeyName.SkipCompanyNumberEntityCodeCreation, SkipCompanyNumberEntityCodeCreation },
            };
        }     

        public string AcceptedEntityType { get; set; }
        public string LookupVocabularyKey { get; set; }
        public bool SkipCompanyNumberEntityCodeCreation { get; set; }
    }
}
