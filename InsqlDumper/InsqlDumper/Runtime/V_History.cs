using LinqToDB.Mapping;
using System;

namespace InsqlDumper.Database
{
	[Table(Name = "v_History")]
	public class V_History
	{
		[Column, NotNull]
		public DateTime DateTime { get; set; }

		[Column, NotNull]
		public string TagName { get; set; }

		[Column]
		public float? Value { get; set; }
			
		[Column]
		public string VValue { get; set; }

		[Column]
		public short? Quality { get; set; }

		[Column]
		public int? OPCQuality { get; set; }

		[Column]
		public int? WwTagKey { get; set; }

		[Column]
		public int? WwResolution { get; set; }

		[Column]
		public string WwEdgeDetection { get; set; }

		[Column]
		public string WwRetrievalMode { get; set; }

		[Column]
		public int? WwTimeDeadband { get; set; }

		[Column]
		public float? WwValueDeadband { get; set; }

		[Column]
		public string WwTimeZone { get; set; }

		[Column]
		public string WwVersion { get; set; }

		[Column]
		public string WwParameters { get; set; }
	}
}