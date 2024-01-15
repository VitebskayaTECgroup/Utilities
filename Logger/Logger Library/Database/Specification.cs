using LinqToDB.Mapping;

namespace Logger_Library.Database
{
	[Table(Name = "Specifications")]
	public class Specification
	{
		[Column, PrimaryKey]
		public int Id { get;set; }

		[Column]
		public int AgentId { get; set; }

		[Column]
		public string Computer { get; set; }

		[Column]
		public string Category { get; set; }

		[Column]
		public string Device { get; set; }

		[Column]
		public string Field { get; set; }

		[Column]
		public string Value { get; set; }
	}
}
