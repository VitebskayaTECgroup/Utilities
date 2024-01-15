using LinqToDB.Mapping;
using System;

namespace InsqlDumper.WWALMDB
{
	[Table(Name = "Comment")]
	public class CommentTable
	{
		[Column, Identity, NotNull] public int CommentId { get; set; }
		[Column] public string OperatorNode { get; set; }
		[Column] public string OperatorName { get; set; }
		[Column] public string Comment { get; set; }
		[Column, NotNull] public DateTime CommentTime { get; set; }
		[Column, NotNull] public short CommentTimeFracSec { get; set; }
		[Column, NotNull] public short CommentTimeZoneOffset { get; set; }
		[Column, NotNull] public short CommentDaylightAdjustment { get; set; }
	}
}
