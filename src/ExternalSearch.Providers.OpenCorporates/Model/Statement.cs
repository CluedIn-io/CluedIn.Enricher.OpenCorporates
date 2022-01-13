using System.Collections.Generic;
using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class Statement
	{

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("data_type")]
		public string DataType { get; set; }

		[JsonProperty("properties")]
		public Properties Properties { get; set; }

		[JsonProperty("opencorporates_url")]
		public string OpencorporatesUrl { get; set; }

		[JsonProperty("start_date")]
		public string StartDate { get; set; }

		[JsonProperty("start_date_type")]
		public object StartDateType { get; set; }

		[JsonProperty("end_date")]
		public object EndDate { get; set; }

		[JsonProperty("end_date_type")]
		public object EndDateType { get; set; }

		[JsonProperty("sample_date")]
		public string SampleDate { get; set; }

		[JsonProperty("predicate")]
		public string Predicate { get; set; }

		[JsonProperty("subject_entities")]
		public List<SubjectEntity> SubjectEntities { get; set; }

		[JsonProperty("object_entities")]
		public List<ObjectEntity> ObjectEntities { get; set; }

		[JsonProperty("sources")]
		public List<SourceStatement> Sources { get; set; }
	}
}