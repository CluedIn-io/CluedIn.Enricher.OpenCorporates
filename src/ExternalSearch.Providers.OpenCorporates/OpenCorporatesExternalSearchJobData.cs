﻿using System.Collections.Generic;
using CluedIn.Core.Crawling;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates
{
    public class OpenCorporatesExternalSearchJobData : CrawlJobData
    {
        public OpenCorporatesExternalSearchJobData(IDictionary<string, object> configuration)
        {
            TargetApiKey = GetValue<string>(configuration, nameof(TargetApiKey), default(string));
            AcceptedEntityType = GetValue(configuration, nameof(AcceptedEntityType), default(string));
            LookupVocabularyKey = GetValue<string>(configuration, nameof(LookupVocabularyKey), default(string));
        }

        public IDictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>()
            {
                { nameof(TargetApiKey), TargetApiKey },
                { nameof(AcceptedEntityType), AcceptedEntityType },
                { nameof(LookupVocabularyKey), LookupVocabularyKey },
            };
        }     

        public string AcceptedEntityType { get; set; }
        public string LookupVocabularyKey { get; set; }
    }
}
