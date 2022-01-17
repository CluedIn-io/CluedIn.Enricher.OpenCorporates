using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class HomeCompany
	{
		[JsonProperty("jurisdiction_code")]
		public string jurisdiction_code { get; set; }

		[JsonProperty("company_number")]
		public string company_number { get; set; }

		[JsonProperty("opencorporates_url")]
		public string opencorporates_url { get; set; }
	}
}