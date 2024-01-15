using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "_ДОК_")]
	public class ДОК
	{
		[Column]
		public DateTime DateRec { get; set; }

		[Column]
		public double? TвремяП22 { get; set; }

		[Column]
		public double? PП22 { get; set; }

		[Column]
		public double? tП22 { get; set; }

		[Column]
		public double? GП22 { get; set; }

		[Column]
		public double? GkП22 { get; set; }

		[Column]
		public double? QП22 { get; set; }

		[Column]
		public double? TвремяП33 { get; set; }

		[Column]
		public double? PП33 { get; set; }

		[Column]
		public double? tП33 { get; set; }

		[Column]
		public double? GП33 { get; set; }

		[Column]
		public double? GkП33 { get; set; }

		[Column]
		public double? QП33 { get; set; }

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