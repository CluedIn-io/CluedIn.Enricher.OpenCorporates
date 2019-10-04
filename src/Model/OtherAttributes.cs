using System.Collections.Generic;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class OtherAttributes
	{
		public Links links { get; set; }
		public string category { get; set; }
		public DescriptionValues description_values { get; set; }
		public string action_date { get; set; }
		public string description { get; set; }
		public string barcode { get; set; }
		public List<Resolution> resolutions { get; set; }
	}
}