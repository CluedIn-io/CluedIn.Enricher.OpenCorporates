using System.Collections.Generic;

namespace CluedIn.ExternalSearch.Providers.OpenCorporates.Model
{
	public class DescriptionValues
	{
		public string date { get; set; }
		public List<Capital> capital { get; set; }
		public string description { get; set; }
	}
}