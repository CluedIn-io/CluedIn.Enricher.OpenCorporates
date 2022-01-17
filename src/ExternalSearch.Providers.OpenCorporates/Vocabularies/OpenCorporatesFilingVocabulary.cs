// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OpenCorporatesFilingVocabulary.cs" company="Clued In">
//   Copyright (c) 2019 Clued In. All rights reserved.
// </copyright>
// <summary>
//   Implements the open corporates filing vocabulary class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Vocabularies
{
    public class OpenCorporatesFilingVocabulary : SimpleVocabulary
    {
        public OpenCorporatesFilingVocabulary()
        {
            this.VocabularyName = "OpenCorporates Filing";
            this.KeyPrefix      = "openCorporates.filing";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Activity;

            this.AddGroup("OpenCorporates Filling Details", group =>
            {
                this.Score             = group.Add(new VocabularyKey("score"));
                this.FilingCode        = group.Add(new VocabularyKey("filingCode"));
                this.FilingType        = group.Add(new VocabularyKey("filingType"));
                this.OpenCorporatesUrl = group.Add(new VocabularyKey("openCorporatesUrl"));
                this.Uid               = group.Add(new VocabularyKey("uid"));
                this.Date              = group.Add(new VocabularyKey("date"));
            });

            this.AddMapping(this.Date, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInDates.CreatedDate);
        }

        public VocabularyKey Score { get; set; }
        public VocabularyKey FilingCode { get; internal set; }
        public VocabularyKey FilingType { get; internal set; }
        public VocabularyKey OpenCorporatesUrl { get; internal set; }
        public VocabularyKey Uid { get; internal set; }
        public VocabularyKey Date { get; internal set; }
    }
}