using System;
using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class CorporateGrouping
	{
		[JsonProperty("name")]
		public string   name               { get; set; }

		[JsonProperty("wikipedia_id")]
		public string   wikipedia_id       { get; set; }

		[JsonProperty("opencorporates_url")]
		public string   opencorporates_url { get; set; }

		[JsonProperty("updated_at")]
		public DateTime updated_at         { get; set; }
	}
}