using LinqToDB.Mapping;

namespace Logger_Library.Database
{
	[Table(Name = "Configurations")]
	public class Configuration
	{
		[Column, PrimaryKey]
		public int Id { get;set; }

		[Column]
		public int SpecSettingsId { get; set; }

		[Column]
		public string Name { get; set; }

		[Column]
		public string Description { get; set; }
	}
}
