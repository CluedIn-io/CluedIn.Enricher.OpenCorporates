using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class Document
	{

		[JsonProperty("source_url")]
		public string SourceUrl { get; set; }

		[JsonProperty("source_location")]
		public string SourceLocation { get; set; }

		[JsonProperty("file_format")]
		public string FileFormat { get; set; }

		[JsonProperty("classification")]
		public string Classification { get; set; }
	}
}