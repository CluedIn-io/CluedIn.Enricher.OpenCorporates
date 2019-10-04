using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class Shareholder
	{

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("address")]
		public Address Address { get; set; }

		[JsonProperty("identifier")]
		public string Identifier { get; set; }
	}
}