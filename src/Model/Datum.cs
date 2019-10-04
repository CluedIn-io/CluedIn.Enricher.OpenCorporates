using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class Datum
	{
		[JsonProperty("id")]
		public int    id                 { get; set; }

		[JsonProperty("title")]
		public string title              { get; set; }

		[JsonProperty("data_type")]
		public string data_type          { get; set; }

		[JsonProperty("description")]
		public string description        { get; set; }

		[JsonProperty("opencorporates_url")]
		public string opencorporates_url { get; set; }
	}
}