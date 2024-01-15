using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "_НОВАЯ_ГОРВЛУЧЕСА_")]
	public class НОВАЯ_ГОРВЛУЧЕСА
	{
		[Column]
		public DateTime DateRec { get; set; }

		[Column]
		public double? Gпрям { get; set; }

		[Column]
		public double? Тпрям { get; set; }

		[Column]
		public double? Gобр { get; set; }

		[Column]
		public double? Тобр { get; set; }

		[Column]
		public double? Тист { get; set; }

		[Column]
		public double? Qпрям { get; set; }

		[Column]
		public double? Qобр { get; set; }

		[Column]
		public double? Энтальпия_прямой { get; set; }

		[Column]
		public double? Энтальпия_обратной { get; set; }

		[Column]
		public double? Поправка_к_Тпр_ { get; set; }

		[Column]
		public double? Поправка_к_Тобр { get; set; }

		[Column]
		public double? Разность { get; set; }

		[Column]
		public double? xGпрям { get; set; }

		[Column]
		public double? IТпрям { get; set; }

		[Column]
		public double? xGобр { get; set; }

		[Column]
		public double? IТобр { get; set; }

		[Column]
		public double? xQ { get; set; }

		[Column]
		public double? Погр_пр { get; set; }

		[Column]
		public double? Погр_обр { get; set; }

	}
}