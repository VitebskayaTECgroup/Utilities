using LinqToDB.Mapping;

namespace InsqlDumper.Database
{
	[Table(Name = "QualityMap")]
	public class QualityMap
	{
		[Column] public int? QualityDetail { get; set; }
		[Column] public string QualityString { get; set; }
	}
}
