using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class StatementResult
	{

		[JsonProperty("api_version")]
		public string ApiVersion { get; set; }

		[JsonProperty("results")]
		public ResultsStatement Results { get; set; }
	}
}