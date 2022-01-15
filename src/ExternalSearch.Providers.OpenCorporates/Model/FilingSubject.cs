using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class FilingSubject
	{

		[JsonProperty("entity_type")]
		public string EntityType { get; set; }

		[JsonProperty("entity_properties")]
		public EntityProperties EntityProperties { get; set; }
	}
}