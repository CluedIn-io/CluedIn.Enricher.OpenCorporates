using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class Profit
	{

		[JsonProperty("end_date")]
		public string EndDate { get; set; }

		[JsonProperty("start_date")]
		public string StartDate { get; set; }

		[JsonProperty("value")]
		public string Value { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }
	}
}