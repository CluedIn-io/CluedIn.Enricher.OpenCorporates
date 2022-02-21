using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class ListOfTrademarkRegistration
	{
		[JsonProperty("trademark_registration")]
		public TrademarkRegistration trademark_registration { get; set; }
	}
}