using System;
using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class SourceStatement
	{

		[JsonProperty("source_url")]
		public string SourceUrl { get; set; }

		[JsonProperty("confidence")]
		public int? Confidence { get; set; }

		[JsonProperty("source_type")]
		public string SourceType { get; set; }

		[JsonProperty("actor_type")]
		public string ActorType { get; set; }

		[JsonProperty("log_message")]
		public object LogMessage { get; set; }

		[JsonProperty("created_at")]
		public DateTime CreatedAt { get; set; }
	}
}