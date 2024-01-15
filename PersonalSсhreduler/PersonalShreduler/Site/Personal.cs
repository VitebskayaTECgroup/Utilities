using LinqToDB.Mapping;
using System;

namespace PersonalShreduler.Site
{
	[Table(Name = "Personal")]
	public class Personal
	{
		[Column]
		public int TabId { get; set; }

		[Column]
		public string Family { get; set; }

		[Column]
		public string Guild { get; set; }

		[Column]
		public DateTime BirthDate { get; set; }

		[Column]
		public bool? OnWork { get; set; }
	}
}