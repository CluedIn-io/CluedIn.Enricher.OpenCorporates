using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class ListOfFilling
	{
		[JsonProperty("filing")]
		public Filing filing { get; set; }
	}
}