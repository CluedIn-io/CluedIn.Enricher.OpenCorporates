using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class Address
	{

		[JsonProperty("country_code")]
		public string CountryCode { get; set; }

		[JsonProperty("street_address")]
		public string StreetAddress { get; set; }

		[JsonProperty("locality")]
		public string Locality { get; set; }

		[JsonProperty("postal_code")]
		public string PostalCode { get; set; }
	}
}