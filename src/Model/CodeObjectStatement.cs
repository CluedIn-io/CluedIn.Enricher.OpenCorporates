using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class CodeObjectStatement
	{

		[JsonProperty("code")]
		public string Code { get; set; }

		[JsonProperty("code_scheme_id")]
		public string CodeSchemeId { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("isic_4")]
		public object Isic4 { get; set; }

		[JsonProperty("maps_to")]
		public object MapsTo { get; set; }
	}
}