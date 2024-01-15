using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "_Техническая_")]
	public class Техническая
	{
		[Column]
		public DateTime DateRec { get; set; }

		[Column]
		public double? TвремяП { get; set; }

		[Column]
		public double? Gов { get; set; }

		[Column]
		public double? GовД { get; set; }

		[Column]
		public double? Gок { get; set; }

		[Column]
		public double? GокД { get; set; }

		[Column]
		public double? GзкКО { get; set; }

		[Column]
		public double? GокКО { get; set; }

		[Column]
		public double? Gсбр { get; set; }

		[Column]
		public double? tсбр { get; set; }

		[Column]
		public double? tдек { get; set; }

	}
}