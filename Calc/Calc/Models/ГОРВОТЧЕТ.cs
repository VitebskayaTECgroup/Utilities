using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "_ГОРВОТЧЕТ_")]
	public class ГОРВОТЧЕТ
	{
		[Column]
		public DateTime DateRec { get; set; }

		[Column]
		public double? Qподп { get; set; }

		[Column]
		public double? xТист_св { get; set; }

	}
}