using System.Collections.Generic;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class FillingResults
	{
		public List<Filing> filings { get; set; }
		public int page { get; set; }
		public int per_page { get; set; }
		public int total_count { get; set; }
		public int total_pages { get; set; }
	}
}