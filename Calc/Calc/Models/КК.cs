using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "_KK_")]
	public class _KK_
	{
		[Column]
		public DateTime DateRec { get; set; }

		[Column]
		public double? TвремяП { get; set; }

		[Column]
		public double? PП { get; set; }

		[Column]
		public double? tП { get; set; }

		[Column]
		public double? GП { get; set; }

		[Column]
		public double? GkП { get; set; }

		[Column]
		public double? QП { get; set; }

		[Column]
		public double? TвремяК { get; set; }

		[Column]
		public double? tK { get; set; }

		[Column]
		public double? GK { get; set; }

		[Column]
		public double? QK { get; set; }

		[Column]
		public double? QПОЛ { get; set; }

		[Column]
		public double? tХИ { get; set; }

	}
}