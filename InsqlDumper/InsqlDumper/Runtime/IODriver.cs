using LinqToDB.Mapping;

namespace InsqlDumper.Database
{
	[Table(Name = "IODriver")]
	public class IODriver
	{
		[Column, Identity] public int IODriverKey { get; set; }
		[Column] public int StorageNodeKey { get; set; }
		[Column] public string ComputerName { get; set; }
		[Column] public string AltComputerName { get; set; }
		[Column] public short StoreForwardMode { get; set; }
		[Column] public string StoreForwardPath { get; set; }
		[Column] public int MinMBThreshold { get; set; }
		[Column] public short? Status { get; set; }
	}
}
