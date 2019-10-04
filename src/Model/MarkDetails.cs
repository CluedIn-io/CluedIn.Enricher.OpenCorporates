using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class MarkDetails
	{
		[JsonProperty("mark_text")]
		public string mark_text      { get; set; }

		[JsonProperty("mark_image_url")]
		public object mark_image_url { get; set; }
	}
}