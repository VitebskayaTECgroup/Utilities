using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "_НОВАЯ_ГОРВБАЛАНС_")]
	public class НОВАЯ_ГОРВБАЛАНС
	{
		[Column]
		public DateTime DateRec { get; set; }

		[Column]
		public double? Gпрямой { get; set; }

		[Column]
		public double? Gобратной { get; set; }

		[Column]
		public double? Баланс { get; set; }

		[Column]
		public double? Gпрямой_коррект { get; set; }

		[Column]
		public double? Gобратной_коррект { get; set; }

	}
}