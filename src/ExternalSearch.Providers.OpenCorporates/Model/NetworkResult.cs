namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class NetworkResult
	{
		public string parent_name { get; set; }
		public string parent_opencorporates_url { get; set; }
		public string parent_type { get; set; }
		public string child_name { get; set; }
		public string child_opencorporates_url { get; set; }
		public string child_type { get; set; }
		public string relationship_type { get; set; }
		public RelationshipProperties relationship_properties { get; set; }
	}
}