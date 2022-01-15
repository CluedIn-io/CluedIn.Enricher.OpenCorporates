using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class MostRecent
	{
		[JsonProperty("datum")]
		public Datum datum { get; set; }
	}
}