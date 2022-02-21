using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class EntityProperties
	{

		[JsonProperty("company_number")]
		public string CompanyNumber { get; set; }

		[JsonProperty("jurisdiction_code")]
		public string JurisdictionCode { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}
}