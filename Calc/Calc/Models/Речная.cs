using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "_Речная_")]
	public class Речная
	{
		[Column]
		public DateTime DateRec { get; set; }

		[Column]
		public double? TвремяП { get; set; }

		[Column]
		public double? G189 { get; set; }

		[Column]
		public double? G190 { get; set; }

		[Column]
		public double? Gсум { get; set; }

		[Column]
		public double? G3 { get; set; }

	}
}