using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "_Сырая_")]
	public class Сырая
	{
		[Column]
		public DateTime DateRec { get; set; }

		[Column]
		public double? TвремяП { get; set; }

		[Column]
		public double? G136 { get; set; }

		[Column]
		public double? G141 { get; set; }

		[Column]
		public double? G142 { get; set; }

	}
}