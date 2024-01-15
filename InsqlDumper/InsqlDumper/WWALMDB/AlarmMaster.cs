using LinqToDB.Mapping;
using System;

namespace InsqlDumper.WWALMDB
{
	[Table(Name = "AlarmMaster")]
	public class AlarmMaster
	{
		[Column, Identity] public int AlarmId { get; set; }
		[Column] public string AlarmGuid { get; set; }
		[Column, NotNull] public int AlarmHandle { get; set; }
		[Column, NotNull] public int ProviderId { get; set; }
		[Column] public string GroupName { get; set; }
		[Column] public string TagName { get; set; }
		[Column] public string TagType { get; set; }
		[Column] public string LoggingNode { get; set; }
		[Column, NotNull] public int QueryId { get; set; }
		[Column] public string bActive { get; set; }
		[Column] public float? TimeDelay { get; set; }
		[Column] public int? CauseId { get; set; }
		[Column] public string AlarmClass { get; set; }
		[Column] public string AlarmType { get; set; }
		[Column, NotNull] public short Priority { get; set; }
		[Column] public float? Limit { get; set; }
		[Column] public string LimitString { get; set; }
		[Column] public float? AlarmValue { get; set; }
		[Column] public string ValueString { get; set; }
		[Column, NotNull] public DateTime OriginationTime { get; set; }
		[Column, NotNull] public short OriginationTimeFracSec { get; set; }
		[Column, NotNull] public short OriginationTimeZoneOffset { get; set; }
		[Column, NotNull] public short OriginationDaylightAdjustment { get; set; }
		[Column] public float? User1 { get; set; }
		[Column] public float? User2 { get; set; }
		[Column] public string User3 { get; set; }
	}
}
