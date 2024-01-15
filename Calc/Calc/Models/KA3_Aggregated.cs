using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "KA3_Aggregated")]
	public class KA3_Aggregated
	{
		[Column]
		public DateTime DateTime { get; set; }

		[Column]
		public string Mode { get; set; }

		[Column]
		public decimal K3_VG1_P35b { get; set; }

		[Column]
		public decimal K3_VG1_P36b { get; set; }

		[Column]
		public decimal K3_VG1_P39b { get; set; }

		[Column]
		public decimal K3_VG1_P40b { get; set; }

		[Column]
		public decimal K3_VG2_P88b { get; set; }

		[Column]
		public decimal K3_VG2_P89b { get; set; }

		[Column]
		public decimal K3_VG2_P79b { get; set; }

		[Column]
		public decimal K3_VG2_P80b { get; set; }

		[Column]
		public decimal K3_VG2_P3e { get; set; }

		[Column]
		public decimal K3_VG2_P4e { get; set; }

		[Column]
		public decimal K3_VG1_P24b { get; set; }

		[Column]
		public decimal K3_VG2_P18v { get; set; }

		[Column]
		public decimal K3_VG2_P2g { get; set; }

		[Column]
		public decimal K3_VG2_P3g { get; set; }

		[Column]
		public decimal K3_VG2_P2v { get; set; }

		[Column]
		public decimal K3_VG2_P3v { get; set; }

		[Column]
		public decimal K3_VG2_P5v { get; set; }

		[Column]
		public decimal K3_VG2_P1g { get; set; }

		[Column]
		public decimal K3_VG2_P4g { get; set; }

		[Column]
		public decimal K3_VG2_P5g { get; set; }

		[Column]
		public decimal K3_VG2_P77b { get; set; }

	}
}