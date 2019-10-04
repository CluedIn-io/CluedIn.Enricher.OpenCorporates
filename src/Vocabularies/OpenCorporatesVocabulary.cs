// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OpenCorporatesVocabulary.cs" company="Clued In">
//   Copyright (c) 2019 Clued In. All rights reserved.
// </copyright>
// <summary>
//   Implements the open corporates vocabulary class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Vocabularies
{
    public static class OpenCorporatesVocabulary
    {
        public static OpenCorporatesOrganizationVocabulary  Organization    { get; } = new OpenCorporatesOrganizationVocabulary();

        public static OpenCorporatesFilingVocabulary        Filing          { get; } = new OpenCorporatesFilingVocabulary();
    }
}