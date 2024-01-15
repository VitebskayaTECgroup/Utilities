using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "_K1_ГАЗ_")]
	public class K1_ГАЗ
	{
		[Column]
		public DateTime DateRec { get; set; }

		[Column]
		public double? TвремяП { get; set; }

		[Column]
		public double? PП { get; set; }

		[Column]
		public double? Pпв { get; set; }

		[Column]
		public double? tП { get; set; }

		[Column]
		public double? GП { get; set; }

		[Column]
		public double? GП_ { get; set; }

		[Column]
		public double? tПв { get; set; }

		[Column]
		public double? GПв { get; set; }

		[Column]
		public double? GПв_ { get; set; }

		[Column]
		public double? tуг1 { get; set; }

		[Column]
		public double? tуг2 { get; set; }

		[Column]
		public double? tугC { get; set; }

		[Column]
		public double? tхв { get; set; }

		[Column]
		public double? КАсл { get; set; }

		[Column]
		public double? КАсп { get; set; }

		[Column]
		public double? КАср { get; set; }

		[Column]
		public double? tкал1 { get; set; }

		[Column]
		public double? tкал2 { get; set; }

		[Column]
		public double? tкал3 { get; set; }

		[Column]
		public double? tкал4 { get; set; }

		[Column]
		public double? tкалC { get; set; }

		[Column]
		public double? tгв { get; set; }

		[Column]
		public double? Gн_пр { get; set; }

		[Column]
		public double? tмаз { get; set; }

		[Column]
		public double? Gмаз { get; set; }

	}
}