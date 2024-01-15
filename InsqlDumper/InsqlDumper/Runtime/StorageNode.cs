using LinqToDB.Mapping;

namespace InsqlDumper.Database
{
	[Table(Name = "StorageNode")]
	public class StorageNode
	{
		[Column, Identity] public int StorageNodeKey { get; set; }
		[Column] public string ComputerName { get; set; }
		[Column] public string Description { get; set; }
		[Column] public int DbStatus { get; set; }
		[Column] public int DbModAcquisition { get; set; }
		[Column] public string DbError { get; set; }
		[Column] public int DbModStorage { get; set; }
		[Column] public int DbModServer { get; set; }
		[Column] public int DbModAll { get; set; }
		[Column] public int DbRevision { get; set; }
	}
}
