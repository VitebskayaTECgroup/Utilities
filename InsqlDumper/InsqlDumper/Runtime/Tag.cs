using LinqToDB.Mapping;
using System;

namespace InsqlDumper.Database
{
	[Table(Name = "Tag")]
	public class Tag
	{
		[Column, NotNull]
		public string TagName { get; set; }

		[Column]
		public int? IOServerKey { get; set; }

		[Column, NotNull]
		public int StorageNodeKey { get; set; }

		[Column, NotNull]
		public int WwTagKey { get; set; }

		[Column]
		public int? TopicKey { get; set; }

		[Column]
		public string Description { get; set; }

		[Column, NotNull]
		public short AcquisitionType { get; set; }

		[Column, NotNull]
		public short StorageType { get; set; }

		[Column, NotNull]
		public int? AcquisitionRate { get; set; }

		[Column]
		public int StorageRate { get; set; }

		[Column]
		public string ItemName { get; set; }

		[Column, NotNull]
		public int TagType { get; set; }

		[Column]
		public int? TimeDeadband { get; set; }

		[Column, NotNull]
		public DateTime DateCreated { get; set; }

		[Column, NotNull]
		public string CreatedBy { get; set; }

		[Column]
		public int? CurrentEditor { get; set; }

		[Column]
		public int? SamplesInActiveImage { get; set; }

		[Column]
		public short? AIRetrievalMode { get; set; }

		[Column]
		public short? Status { get; set; }

		[Column]
		public int? CalculatedAISamples { get; set; }
	}
}