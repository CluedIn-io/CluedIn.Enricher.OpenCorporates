using System;
using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class Filing
	{
		[JsonProperty("id")]
		public int    id                 { get; set; }

		[JsonProperty("title")]
		public string title              { get; set; }

		[JsonProperty("description")]
		public string description        { get; set; }

		[JsonProperty("uid")]
		public string uid                { get; set; }

		[JsonProperty("filing_type_code")]
		public string filing_type_code   { get; set; }

		[JsonProperty("filing_type_name")]
		public string filing_type_name   { get; set; }

		[JsonProperty("url")]
		public object url                { get; set; }

		[JsonProperty("opencorporates_url")]
		public string opencorporates_url { get; set; }

		[JsonProperty("date")]
		public DateTimeOffset? date               { get; set; }
	}
}