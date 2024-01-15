using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "_НОВАЯ_ГОРВОТЧЕТ_")]
	public class НОВАЯ_ГОРВОТЧЕТ
	{
		[Column]
		public DateTime DateRec { get; set; }

		[Column]
		public double? Qподп { get; set; }

		[Column]
		public double? xТист_св { get; set; }

	}
}