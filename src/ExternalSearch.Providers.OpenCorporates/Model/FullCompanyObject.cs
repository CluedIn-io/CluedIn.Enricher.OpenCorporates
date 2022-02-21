using System.Collections.Generic;
using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class FullCompanyObject
	{
		[JsonProperty("companies")]
		public List<CompanyList> companies { get; set; }
		public Company Company { get; set; }
	}
}