using LinqToDB.Mapping;
using System;

namespace InsqlDumper.WWALMDB
{
	[Table(Name = "Events")]
	public class Events
	{
		[Column, Identity, NotNull] public int EventId { get; set; }
		[Column] public string EventGuid { get; set; }
		[Column, NotNull] public int ProviderId { get; set; }
		[Column] public string GroupName { get; set; }
		[Column] public string TagName { get; set; }
		[Column] public string EventClass { get; set; }
		[Column] public string EventType { get; set; }
		[Column] public string EventState { get; set; }
		[Column, NotNull] public short EventPriority { get; set; }
		[Column] public float? EventValue { get; set; }
		[Column] public float? EventLimit { get; set; }
		[Column] public string ValueString { get; set; }
		[Column] public string LimitString { get; set; }
		[Column, NotNull] public DateTime EventTime { get; set; }
		[Column, NotNull] public short EventTimeFracSec { get; set; }
		[Column, NotNull] public short EventTimeZoneOffset { get; set; }
		[Column, NotNull] public short EventDaylightAdjustment { get; set; }
		[Column] public string OperatorNode { get; set; }
		[Column] public string OperatorName { get; set; }
		[Column] public string Comment { get; set; }
		[Column] public float? User1 { get; set; }
		[Column] public float? User2 { get; set; }
		[Column] public string User3 { get; set; }
		[Column] public int? OperatorID { get; set; }
	}
}
