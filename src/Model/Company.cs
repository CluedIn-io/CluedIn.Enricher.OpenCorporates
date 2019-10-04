using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class Company
	{
		[JsonProperty("name")]
		public string name { get; set; }

		[JsonProperty("company_number")]
		public string company_number { get; set; }

		[JsonProperty("jurisdiction_code")]
		public string jurisdiction_code { get; set; }

		[JsonProperty("incorporation_date")]
		public string incorporation_date { get; set; }

		[JsonProperty("dissolution_date")]
		public string dissolution_date { get; set; }

		[JsonProperty("company_type")]
		public string company_type { get; set; }

		[JsonProperty("registry_url")]
		public string registry_url { get; set; }

		[JsonProperty("branch")]
		public string branch { get; set; }

		[JsonProperty("branch_status")]
		public string branch_status { get; set; }

		[JsonProperty("inactive")]
		public bool? inactive { get; set; }

		[JsonProperty("current_status")]
		public string current_status { get; set; }

		[JsonProperty("created_at")]
		public DateTime created_at { get; set; }

		[JsonProperty("updated_at")]
		public DateTime updated_at { get; set; }

		[JsonProperty("retrieved_at")]
		public DateTime retrieved_at { get; set; }

		[JsonProperty("opencorporates_url")]
		public string opencorporates_url { get; set; }

		[JsonProperty("source")]
		public Source source { get; set; }

		[JsonProperty("agent_name")]
		public string agent_name { get; set; }

		[JsonProperty("agent_address")]
		public string agent_address { get; set; }

		[JsonProperty("alternative_names")]
		public List<AlternativeName> alternative_names { get; set; }

		[JsonProperty("previous_names")]
		public List<PreviousName> previous_names { get; set; }

		[JsonProperty("number_of_employees")]
		public string number_of_employees { get; set; }

		[JsonProperty("native_company_number")]
		public string native_company_number { get; set; }

		[JsonProperty("registered_address_in_full")]
		public string registered_address_in_full { get; set; }

		[JsonProperty("industry_codes")]
		public List<IndustryCodeItem> industry_codes { get; set; }

		[JsonProperty("identifiers")]
		public List<Identifiers> identifiers { get; set; }

		[JsonProperty("trademark_registrations")]
		public List<ListOfTrademarkRegistration> trademark_registrations { get; set; }

		[JsonProperty("corporate_groupings")]
		public List<ListOfCorporateGrouping> corporate_groupings { get; set; }

		[JsonProperty("data")]
		public Data data { get; set; }

		[JsonProperty("home_company")]
		public HomeCompany home_company { get; set; }

		[JsonProperty("filings")]
		public List<ListOfFilling> filings { get; set; }

		[JsonProperty("officers")]
		public List<ListOfOfficer> officers { get; set; }

	}
}