using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class IndustryCode
	{
		[JsonProperty("code")]
		public string code { get; set; }

		[JsonProperty("description")]
		public string description { get; set; }

		[JsonProperty("code_scheme_id")]
		public string code_scheme_id { get; set; }

		[JsonProperty("code_scheme_name")]
		public string code_scheme_name { get; set; }

		[JsonProperty("uid")]
		public string uid { get; set; }
	}
}