﻿using System.Collections.Generic;
using CluedIn.Core.Crawling;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates
{
    public class OpenCorporatesExternalSearchJobData : CrawlJobData
    {
        public OpenCorporatesExternalSearchJobData(IDictionary<string, object> configuration)
        {
            TargetApiKey = GetValue<string>(configuration, nameof(TargetApiKey), default(string));
        }

        public IDictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>()
            {
                { nameof(TargetApiKey), TargetApiKey }
            };
        }     
    }
}
