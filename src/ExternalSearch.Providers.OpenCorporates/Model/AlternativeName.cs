using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class AlternativeName
	{
		[JsonProperty("company_name")]
		public string company_name { get; set; }

		[JsonProperty("start_date")]
		public string start_date { get; set; }

		[JsonProperty("type")]
		public string type { get; set; }
	}
}