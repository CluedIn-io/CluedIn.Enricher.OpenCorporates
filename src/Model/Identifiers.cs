using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class Identifiers
	{
		[JsonProperty("identifier_system_code")]
		public string identifier_system_code { get; set; }

		[JsonProperty("identifier_system_name")]
		public string identifier_system_name { get; set; }
	}
}