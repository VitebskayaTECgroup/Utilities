using LinqToDB.Mapping;

namespace InsqlDumper.Database
{
	[Table(Name = "Topic")]
	public class Topic
	{
		[Column, Identity] public int TopicKey { get; set; }
		[Column] public int IOServerKey { get; set; }
		[Column] public int StorageNodeKey { get; set; }
		[Column] public string Name { get; set; }
		[Column] public int TimeOut { get; set; }
		[Column] public short? Status { get; set; }
	}
}
