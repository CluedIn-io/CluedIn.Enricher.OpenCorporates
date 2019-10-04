using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class ObjectEntity
	{

		[JsonProperty("entity_type")]
		public string EntityType { get; set; }

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("opencorporates_url")]
		public string OpencorporatesUrl { get; set; }

		[JsonProperty("company_number")]
		public string CompanyNumber { get; set; }

		[JsonProperty("jurisdiction_code")]
		public string JurisdictionCode { get; set; }
	}
}