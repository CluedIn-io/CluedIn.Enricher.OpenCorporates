using System.Collections.Generic;
using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class ResultsStatement
	{

		[JsonProperty("statements")]
		public List<StatementObject> Statements { get; set; }

		[JsonProperty("page")]
		public int Page { get; set; }

		[JsonProperty("per_page")]
		public int PerPage { get; set; }

		[JsonProperty("total_pages")]
		public int TotalPages { get; set; }

		[JsonProperty("total_count")]
		public int TotalCount { get; set; }
	}
}