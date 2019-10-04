using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class TrademarkRegistration
	{
		[JsonProperty("mark_details")]
		public MarkDetails mark_details       { get; set; }

		[JsonProperty("register_name")]
		public string      register_name      { get; set; }

		[JsonProperty("registration_date")]
		public string      registration_date  { get; set; }

		[JsonProperty("expiry_date")]
		public string      expiry_date        { get; set; }

		[JsonProperty("opencorporates_url")]
		public string      opencorporates_url { get; set; }
	}
}