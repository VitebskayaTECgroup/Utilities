using LinqToDB.Mapping;
using System;

namespace Logger_Library.Database
{
	[Table(Name = "Agents")]
	public class Agent
	{
		[Column, PrimaryKey]
		public int Id { get;set; }

		[Column]
		public string Endpoint { get; set; }

		[Column]
		public int ConfigurationId { get; set; }

		[Column]
		public DateTime LastTimeAlive { get; set; }

		[Column]
		public string Description { get; set; }
	}
}
