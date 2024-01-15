using LinqToDB.Mapping;

namespace InsqlDumper.Database
{
	[Table(Name = "IOServer")]
	public class IOServer
	{
		[Column, Identity] public int IOServerKey { get; set; }
		[Column] public int StorageNodeKey { get; set; }
		[Column] public int? IODriverKey { get; set; }
		[Column] public string ApplicationName { get; set; }
		[Column] public string Path { get; set; }
		[Column] public string ComputerName { get; set; }
		[Column] public string AltComputerName { get; set; }
		[Column] public bool AutoStart { get; set; }
		[Column] public int ExeType { get; set; }
		[Column] public short InitializationStatus { get; set; }
		[Column] public short ProtocolType { get; set; }
		[Column] public string Description { get; set; }
		[Column] public short? Status { get; set; }
	}
}
