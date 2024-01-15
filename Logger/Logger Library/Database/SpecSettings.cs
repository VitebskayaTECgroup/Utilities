using LinqToDB.Mapping;

namespace Logger_Library.Database
{
	[Table(Name = "SpecSettings")]
	public class SpecSettings
	{
		[Column, PrimaryKey]
		public int Id { get;set; }

		[Column]
		public bool CheckDisks { get; set; }

		[Column]
		public bool CheckProcessors { get; set; }

		[Column]
		public bool CheckMotherboard { get; set; }

		[Column]
		public bool CheckVideocards { get; set; }

		[Column]
		public bool CheckRam { get; set; }

		[Column]
		public bool CheckSoftware { get; set; }

		[Column]
		public bool CheckAutorun { get; set; }
	}
}
