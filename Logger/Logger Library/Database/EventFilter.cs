using LinqToDB.Mapping;

namespace Logger_Library.Database
{
	[Table(Name = "EventFilters")]
	public class EventFilter
	{
		[Column, PrimaryKey]
		public int Id { get;set; }

		[Column]
		public string Name { get; set; }

		[Column]
		public string Description { get; set; }

		[Column]
		public bool Allow { get; set; }

		[Column]
		public string Journals { get; set; }

		[Column]
		public string Sources { get; set; }

		[Column]
		public string EventIds { get; set; }

		[Column]
		public string Categories { get; set; }

		[Column]
		public string Types { get; set; }

		[Column]
		public string Computers { get; set; }

	}
}
