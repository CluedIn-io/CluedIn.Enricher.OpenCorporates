﻿using System;
using System.Collections.Generic;
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

        public static AuthMethods AuthMethods { get; set; } = new AuthMethods
        {
            token = new List<Control>()
        };

        public static IEnumerable<Control> Properties { get; set; } = new List<Control>()
        {
            // NOTE: Leaving this commented as an example - BF
            //new()
            //{
            //    displayName = "Some Data",
            //    type = "input",
            //    isRequired = true,
            //    name = "someData"
            //}
        };

        public static Guide Guide { get; set; } = null;
        public static IntegrationType IntegrationType { get; set; } = IntegrationType.Enrichment;
    }
}