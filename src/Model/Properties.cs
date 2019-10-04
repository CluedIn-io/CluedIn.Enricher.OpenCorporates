using System.Collections.Generic;
using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class Properties
	{

		[JsonProperty("code_object")]
		public CodeObjectStatement CodeObject { get; set; }

		[JsonProperty("percent_certainty")]
		public string PercentCertainty { get; set; }

		[JsonProperty("percentage_of_shares_max")]
		public double? PercentageOfSharesMax { get; set; }

		[JsonProperty("percentage_of_shares_min")]
		public int? PercentageOfSharesMin { get; set; }

		[JsonProperty("shareholders")]
		public IList<Shareholder> Shareholders { get; set; }

		[JsonProperty("voting_percentage_max")]
		public double? VotingPercentageMax { get; set; }

		[JsonProperty("voting_percentage_min")]
		public int? VotingPercentageMin { get; set; }

		[JsonProperty("accounts_date")]
		public string AccountsDate { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("current_assets")]
		public List<CurrentAsset> CurrentAssets { get; set; }

		[JsonProperty("profit")]
		public List<Profit> Profit { get; set; }

		[JsonProperty("revenue")]
		public object Revenue { get; set; }

		[JsonProperty("accounts_type")]
		public string AccountsType { get; set; }

		[JsonProperty("documents")]
		public List<Document> Documents { get; set; }

		[JsonProperty("filing_date")]
		public string FilingDate { get; set; }

		[JsonProperty("filing_subject")]
		public FilingSubject FilingSubject { get; set; }

		[JsonProperty("filing_type_name")]
		public string FilingTypeName { get; set; }

		[JsonProperty("language")]
		public string Language { get; set; }

		[JsonProperty("total_assets")]
		public List<TotalAsset> TotalAssets { get; set; }

		[JsonProperty("uid")]
		public string Uid { get; set; }
	}
}