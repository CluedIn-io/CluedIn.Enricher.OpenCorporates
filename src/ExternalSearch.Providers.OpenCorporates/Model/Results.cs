using System.Collections.Generic;
using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class Results
	{
		[JsonProperty("companies")]
		public List<CompaniesList> companies { get; set; }

		[JsonProperty("company")]
		public Company company { get; set; }
	}
}