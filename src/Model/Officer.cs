using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class Officer
	{
		[JsonProperty("id")]
		public int id { get; set; }

		[JsonProperty("name")]
		public string name { get; set; }

		[JsonProperty("position")]
		public string position { get; set; }

		[JsonProperty("uid")]
		public string uid { get; set; }

		[JsonProperty("start_date")]
		public string start_date { get; set; }

		[JsonProperty("end_date")]
		public string end_date { get; set; }

		[JsonProperty("opencorporates_url")]
		public string opencorporates_url { get; set; }

		[JsonProperty("occupation")]
		public string occupation { get; set; }

		[JsonProperty("innactive")]
		public bool innactive { get; set; }

		[JsonProperty("current_status")]
		public string current_status { get; set; }
	}
}