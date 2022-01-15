using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class IndustryCodeItem
	{
		[JsonProperty("industry_code")]
		public IndustryCode IndustryCode { get; set; }
	}
}