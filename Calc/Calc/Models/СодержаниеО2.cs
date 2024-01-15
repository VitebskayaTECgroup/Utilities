using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "_СодержаниеO2_")]
	public class СодержаниеO2
	{
		[Column]
		public DateTime DateRec { get; set; }

		[Column]
		public double? TвремяП { get; set; }

		[Column]
		public double? ТА2 { get; set; }

		[Column]
		public double? ТА3 { get; set; }

		[Column]
		public double? Др12 { get; set; }

		[Column]
		public double? Др6N1 { get; set; }

		[Column]
		public double? Др6N2 { get; set; }

		[Column]
		public double? Др6N3 { get; set; }

	}
}