using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class TotalAsset
	{

		[JsonProperty("date")]
		public string Date { get; set; }

		[JsonProperty("value")]
		public string Value { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }
	}
}