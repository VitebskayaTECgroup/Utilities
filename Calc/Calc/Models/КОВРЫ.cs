using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "_КОВРЫ_")]
	public class КОВРЫ
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
		public double? GП_ { get; set; }

		[Column]
		public double? GkП_ { get; set; }

		[Column]
		public double? QП_ { get; set; }

		[Column]
		public double? TвремяК { get; set; }

		[Column]
		public double? tK { get; set; }

		[Column]
		public double? PК { get; set; }

		[Column]
		public double? GK_ { get; set; }

		[Column]
		public double? QK_ { get; set; }

		[Column]
		public double? GПкор { get; set; }

		[Column]
		public double? QПкор { get; set; }

		[Column]
		public double? GKкор { get; set; }

		[Column]
		public double? Qkкор { get; set; }

		[Column]
		public double? dQХИП { get; set; }

		[Column]
		public double? dQХИК { get; set; }

		[Column]
		public double? QПКОМ { get; set; }

		[Column]
		public double? QККОМ { get; set; }

		[Column]
		public double? QПОЛКОМ { get; set; }

	}
}