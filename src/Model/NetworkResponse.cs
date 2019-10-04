using System.Collections.Generic;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class NetworkResponse
	{
		public string api_version { get; set; }
		public List<NetworkResult> results { get; set; }
	}
}