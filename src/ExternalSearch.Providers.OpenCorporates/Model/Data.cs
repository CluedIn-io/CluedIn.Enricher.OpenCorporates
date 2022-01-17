using System.Collections.Generic;
using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class Data
	{
		[JsonProperty("most_recent")]
		public List<MostRecent> most_recent { get; set; }

		[JsonProperty("total_count")]
		public int               total_count { get; set; }

		[JsonProperty("url")]
		public string url { get; set; }
	}
}