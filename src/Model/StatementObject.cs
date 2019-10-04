using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class StatementObject
	{

		[JsonProperty("statement")]
		public Statement Statement { get; set; }
	}
}