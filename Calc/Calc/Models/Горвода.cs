using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "_Горвода_")]
	public class Горвода
	{
		[Column]
		public DateTime DateRec { get; set; }

		[Column]
		public double? TвремяП { get; set; }

		[Column]
		public double? G { get; set; }

		[Column]
		public double? GМ { get; set; }

		[Column]
		public double? Gч { get; set; }

		[Column]
		public double? Gs { get; set; }

		[Column]
		public double? GMs { get; set; }

		[Column]
		public double? Gчs { get; set; }

	}
}