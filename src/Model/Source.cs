using System;
using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class Source
	{
		[JsonProperty("publisher")]
		public string publisher { get; set; }

		[JsonProperty("url")]
		public string url { get; set; }

		[JsonProperty("retrieved_at")]
		public DateTime retrieved_at { get; set; }
	}
}