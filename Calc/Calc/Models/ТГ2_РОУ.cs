using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "_ТГ2_РОУ_")]
	public class ТГ2_РОУ
	{
		[Column]
		public DateTime DateRec { get; set; }

		[Column]
		public double? TвремяП { get; set; }

		[Column]
		public double? tсв { get; set; }

		[Column]
		public double? pr_100_40 { get; set; }

		[Column]
		public double? G40_10 { get; set; }

		[Column]
		public double? G13_6 { get; set; }

		[Column]
		public double? G40_6 { get; set; }

		[Column]
		public double? pr_100_13 { get; set; }

		[Column]
		public double? pr_100_13_ { get; set; }

		[Column]
		public double? Gдрб { get; set; }

		[Column]
		public double? Gдрб_дубл { get; set; }

		[Column]
		public double? Gдрб_сумм { get; set; }

	}
}