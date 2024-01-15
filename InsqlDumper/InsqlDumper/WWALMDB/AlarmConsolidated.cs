using LinqToDB.Mapping;
using System;

namespace InsqlDumper.WWALMDB
{
	[Table(Name = "AlarmConsolidated")]
	public class AlarmConsolidated
	{
		[Column, NotNull] public int AlarmId { get; set; }
		[Column, NotNull] public string AlarmType { get; set; }
		[Column, NotNull] public short Priority { get; set; }
		[Column] public float? Limit { get; set; }
		[Column] public string LimitString { get; set; }
		[Column] public float? AlarmValue { get; set; }
		[Column] public string ValueString { get; set; }
		[Column, NotNull] public DateTime AlarmTime { get; set; }
		[Column, NotNull] public short AlarmTimeFracSec { get; set; }
		[Column, NotNull] public short AlarmTimeZoneOffset { get; set; }
		[Column, NotNull] public short AlarmDaylightAdjustment { get; set; }
		[Column] public int? CommentId { get; set; }
		[Column] public short? OutstandingAcks { get; set; }
		[Column] public DateTime? AckTime { get; set; }
		[Column] public short? AckTimeFracSec { get; set; }
		[Column] public short? AckTimeZoneOffset { get; set; }
		[Column] public short? AckDaylightAdjustment { get; set; }
		[Column, NotNull] public string AckOperatorName { get; set; }
		[Column, NotNull] public string AckNodeName { get; set; }
		[Column] public int? AckCommentId { get; set; }
		[Column] public float? ReturnValue { get; set; }
		[Column, NotNull] public string ReturnValueString { get; set; }
		[Column] public DateTime? ReturnTime { get; set; }
		[Column] public short? ReturnTimeFracSec { get; set; }
		[Column] public short? ReturnTimeZoneOffset { get; set; }
		[Column] public short? ReturnDaylightAdjustment { get; set; }
		[Column] public float? User1 { get; set; }
		[Column] public float? User2 { get; set; }
		[Column] public string User3 { get; set; }
		[Column] public int? Cookie { get; set; }
		[Column] public string OperatorName { get; set; }
		[Column] public string OperatorNode { get; set; }
		[Column] public string UnAckDuration { get; set; }
		[Column] public int? OperatorID { get; set; }
		[Column] public int? AckOperatorID { get; set; }
	}
}
