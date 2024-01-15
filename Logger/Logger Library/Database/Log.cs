using LinqToDB.Mapping;
using System;

namespace Logger_Library.Database
{
	[Table(Name = "Logs")]
	public class Log
	{
		[Column, PrimaryKey]
		public int Id { get;set; }

		[Column]
		public int AgentId { get; set; }

		[Column]
		public DateTime TimeStamp { get; set; }

		[Column]
		public string Journal { get; set; }

		[Column]
		public string Source { get; set; }

		[Column]
		public int EventId { get; set; }

		[Column]
		public string Category { get; set; }

		[Column]
		public string Type { get; set; }

		[Column]
		public string Computer { get; set; }

		[Column]
		public string Username { get; set; }

		[Column]
		public string Message { get; set; }

	}
}
