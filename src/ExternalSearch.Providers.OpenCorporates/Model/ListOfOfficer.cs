using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class ListOfOfficer
	{
		[JsonProperty("officer")]
		public Officer Officer { get; set; }
	}
}