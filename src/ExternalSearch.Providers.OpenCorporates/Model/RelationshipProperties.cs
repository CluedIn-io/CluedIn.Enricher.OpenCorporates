namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class RelationshipProperties
	{
		public string percent_certainty { get; set; }
		public string latest_date { get; set; }
		public int confidence { get; set; }
		public string latest_date_type { get; set; }
		public string earliest_date_type { get; set; }
		public double ownership_percentage { get; set; }
		public string earliest_date { get; set; }
		public bool is_deletion { get; set; }
		public int? number_of_shares { get; set; }
	}
}