// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OpenCorporatesUtil.cs" company="Clued In">
//   Copyright (c) 2019 Clued In. All rights reserved.
// </copyright>
// <summary>
//   Implements the open corporates utility class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

using CluedIn.Core;
using CluedIn.Core.Data.Vocabularies;
using CluedIn.ExternalSearch.Providers.OpenCorporates.Vocabularies;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates
{
    /// <summary>
    /// OpenCorporates Helper Class
    /// </summary>
    public class OpenCorporatesUtil
    {
        /// <summary>
        /// Returns jurisdiction based on VocabularyKey.
        /// </summary>
        /// <param name="request">ExternalSearch Request</param>
        /// <returns>Jurisdiction and Code</returns>
        public static Dictionary<string, string> GetAllCodesFromRequest(IExternalSearchRequest request)
        {
            var companyCodeVocabKeys = new[] {
                CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.CodesCVR,
                CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.CodesBrreg,
                CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.CodesCompanyHouse,
                CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.CodesCIK
            };

            var keyJurisdictionCollection = new Dictionary<string, string>();

            foreach (var codeVocabKey in companyCodeVocabKeys)
            {
                var identifierCode = request.QueryParameters.GetValue(codeVocabKey, new HashSet<string>());

                if (identifierCode.Any())
                {
                    keyJurisdictionCollection[JurisdictionCode(codeVocabKey)] = identifierCode.FirstOrDefault();
                }
                else
                    continue;
            }

            {
                var companyNumber       = request.QueryParameters.GetValue(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.CodesCompanyNumber, new HashSet<string>());
                var jurisdictionCode    = request.QueryParameters.GetValue(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.JurisdictionCode, new HashSet<string>());

                if (companyNumber.Any() && jurisdictionCode.Any())
                    keyJurisdictionCollection[jurisdictionCode.First()] = companyNumber.First();
            }

            {
                var companyNumber       = request.QueryParameters.GetValue(OpenCorporatesVocabulary.Organization.CompanyNumber, new HashSet<string>());
                var jurisdictionCode    = request.QueryParameters.GetValue(OpenCorporatesVocabulary.Organization.JurisdictionCode, new HashSet<string>());

                if (companyNumber.Any() && jurisdictionCode.Any())
                    keyJurisdictionCollection[jurisdictionCode.First()] = companyNumber.First();
            }

            return keyJurisdictionCollection;
        }

        private static string JurisdictionCode(VocabularyKey vocabularyKey)
        {
            return vocabularyKey.Equals(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization
                    .CodesCVR) ? "dk" :
                vocabularyKey.Equals(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization
                    .CodesCIK) ? "us" :
                vocabularyKey.Equals(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization
                    .CodesBrreg) ? "no" :
                vocabularyKey.Equals(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization
                    .CodesCompanyHouse) ? "gb" : "";
        }
    }
}
