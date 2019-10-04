// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OpenCorporatesResponse.cs" company="Clued In">
//   Copyright (c) 2019 Clued In. All rights reserved.
// </copyright>
// <summary>
//   Implements the open corporates response class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class OpenCorporatesResponse
    {
        [JsonProperty("api_version")]
        public string api_version { get; set; }

        [JsonProperty("results")]
        public Results results { get; set; }
    }
}
