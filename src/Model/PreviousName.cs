using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class PreviousName
	{
		[JsonProperty("start_date")]
		public string start_date { get; set; }

		[JsonProperty("company_name")]
		public string company_name { get; set; }
	}
}