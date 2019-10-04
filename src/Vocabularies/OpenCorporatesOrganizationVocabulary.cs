// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OpenCorporatesOrganizationVocabulary.cs" company="Clued In">
//   Copyright (c) 2019 Clued In. All rights reserved.
// </copyright>
// <summary>
//   Implements the open corporates organization vocabulary class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Vocabularies
{
    public class OpenCorporatesOrganizationVocabulary : SimpleVocabulary
    {
        public OpenCorporatesOrganizationVocabulary()
        {
            this.VocabularyName = "OpenCorporates Organization";
            this.KeyPrefix      = "openCorporates.organization";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Organization;

            this.AddGroup("OpenCorporates Organization Details", group =>
            {
                this.AgentName                  = group.Add(new VocabularyKey("agentName"));
                this.AgentAddress               = group.Add(new VocabularyKey("agentAddress"));
                this.AlternativeNames           = group.Add(new VocabularyKey("alternativeNames"));
                this.Branch                     = group.Add(new VocabularyKey("branch"));
                this.BranchStatus               = group.Add(new VocabularyKey("branchStatus"));
                this.CompanyNumber              = group.Add(new VocabularyKey("companyNumber"));
                this.CompanyType                = group.Add(new VocabularyKey("companyType"));
                this.ControllingEntity          = group.Add(new VocabularyKey("controllingEntity"));
                this.CorporateGroupings         = group.Add(new VocabularyKey("corporateGroupings",         VocabularyKeyDataType.Json,             VocabularyKeyVisibility.Hidden));
                this.CreatedAt                  = group.Add(new VocabularyKey("createdAt",                  VocabularyKeyDataType.DateTime));
                this.CurrentStatus              = group.Add(new VocabularyKey("currentStatus"));
                this.Data                       = group.Add(new VocabularyKey("data",                       VocabularyKeyDataType.Json,             VocabularyKeyVisibility.Hidden));
                this.DissolutionDate            = group.Add(new VocabularyKey("dissolutionDate"));
                this.Filings                    = group.Add(new VocabularyKey("filings",                    VocabularyKeyDataType.Json,             VocabularyKeyVisibility.Hidden));
                this.IncorporationDate          = group.Add(new VocabularyKey("incorporationDate"));
                this.Identifiers                = group.Add(new VocabularyKey("identifiers"));
                this.IndustryCodes              = group.Add(new VocabularyKey("industryCodes"));
                this.NativeCompanyNumber        = group.Add(new VocabularyKey("nativeCompanyNumber"));
                this.RegistryUrl                = group.Add(new VocabularyKey("registryUrl",                VocabularyKeyDataType.Uri));
                this.JurisdictionCode           = group.Add(new VocabularyKey("jurisdictionCode"));
                this.AddressCountryCode         = group.Add(new VocabularyKey("addressCountryCode"));
                this.InActive                   = group.Add(new VocabularyKey("inActive",                   VocabularyKeyDataType.Boolean));
                this.HomeCompany                = group.Add(new VocabularyKey("homeCompany"));
                this.OpenCorporatesUrl          = group.Add(new VocabularyKey("openCorporatesUrl",          VocabularyKeyDataType.Uri));
                this.Officers                   = group.Add(new VocabularyKey("officersName"));
                this.OfficersJson               = group.Add(new VocabularyKey("officers",                   VocabularyKeyDataType.Json,             VocabularyKeyVisibility.Hidden));
                this.NumberOfEmployees          = group.Add(new VocabularyKey("numberOfEmployees"));
                this.SourcePublisher            = group.Add(new VocabularyKey("sourcePublisher"));
                this.SourceUrl                  = group.Add(new VocabularyKey("sourceUrl",                  VocabularyKeyDataType.Uri));
                this.PreviousNames              = group.Add(new VocabularyKey("previousNames"));
                this.RegisteredAddress          = group.Add(new VocabularyKey("registeredAddress"));
                this.UpdatedAt                  = group.Add(new VocabularyKey("updatedAt",                  VocabularyKeyDataType.DateTime));
                this.TrademarkRegistration      = group.Add(new VocabularyKey("trademarkRegistration",      VocabularyKeyDataType.Json,             VocabularyKeyVisibility.Hidden));
                this.Website                    = group.Add(new VocabularyKey("website",                    VocabularyKeyDataType.Uri));
                this.TelephoneNumber            = group.Add(new VocabularyKey("telephoneNumber",            VocabularyKeyDataType.PhoneNumber));
                this.OfficialRegisterEntry      = group.Add(new VocabularyKey("officialRegisterEntry"));
            });

            this.AddMapping(this.CreatedAt,             CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInDates.CreatedDate);
            this.AddMapping(this.UpdatedAt,             CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInDates.ModifiedDate);

            this.AddMapping(this.CompanyNumber,         CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.CodesCompanyNumber);
            this.AddMapping(this.CompanyType,           CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Type);
            this.AddMapping(this.IncorporationDate,     CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.FoundingDate);
            this.AddMapping(this.DissolutionDate,       CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.DissolutionDate);
            this.AddMapping(this.JurisdictionCode,      CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.JurisdictionCode);
            this.AddMapping(this.AddressCountryCode,    CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressCountryCode);
            this.AddMapping(this.NumberOfEmployees,     CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.EmployeeCount);
            this.AddMapping(this.RegisteredAddress,     CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Address);
            this.AddMapping(this.Website,               CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Website);
            this.AddMapping(this.TelephoneNumber,       CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.PhoneNumber);
        }

        public VocabularyKey AgentAddress { get; set; }
        public VocabularyKey AgentName { get; set; }
        public VocabularyKey AlternativeNames { get; set; }
        public VocabularyKey Branch { get; set; }
        public VocabularyKey BranchStatus { get; set; }
        public VocabularyKey CompanyNumber { get; set; }
        public VocabularyKey CompanyType { get; set; }
        public VocabularyKey ControllingEntity { get; set; }
        public VocabularyKey CorporateGroupings { get; set; }
        public VocabularyKey CreatedAt { get; set; }
        public VocabularyKey CurrentStatus { get; set; }
        public VocabularyKey Data { get; set; }
        public VocabularyKey DissolutionDate { get; set; }
        public VocabularyKey Filings { get; set; }
        public VocabularyKey IncorporationDate { get; set; }
        public VocabularyKey Identifiers { get; set; }
        public VocabularyKey IndustryCodes { get; set; }
        public VocabularyKey NativeCompanyNumber { get; set; }
        public VocabularyKey RegistryUrl { get; set; }
        public VocabularyKey JurisdictionCode { get; set; }
        public VocabularyKey InActive { get; set; }
        public VocabularyKey HomeCompany { get; set; }
        public VocabularyKey OpenCorporatesUrl { get; set; }
        public VocabularyKey Officers { get; set; }
        public VocabularyKey OfficersJson { get; set; }
        public VocabularyKey NumberOfEmployees { get; set; }
        public VocabularyKey SourcePublisher { get; set; }
        public VocabularyKey SourceUrl { get; set; }
        public VocabularyKey PreviousNames { get; set; }
        public VocabularyKey RegisteredAddress { get; set; }
        public VocabularyKey UpdatedAt { get; set; }
        public VocabularyKey TrademarkRegistration { get; set; }
        public VocabularyKey Website { get; set; }
        public VocabularyKey TelephoneNumber { get; set; }
        public VocabularyKey OfficialRegisterEntry { get; set; }
        public VocabularyKey AddressCountryCode { get; set; }
    }
}