using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class ListOfCorporateGrouping
	{
		[JsonProperty("corporate_grouping")]
		public CorporateGrouping corporate_grouping { get; set; }
	}
}