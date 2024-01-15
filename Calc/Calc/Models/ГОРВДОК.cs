using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "_ГОРВДОК_")]
	public class ГОРВДОК
	{
		[Column]
		public DateTime DateRec { get; set; }

		[Column]
		public double? Gпрям { get; set; }

		[Column]
		public double? Тпрям { get; set; }

		[Column]
		public double? Gобр1 { get; set; }

		[Column]
		public double? Тобр1 { get; set; }

		[Column]
		public double? Gобр2 { get; set; }

		[Column]
		public double? Тобр2 { get; set; }

		[Column]
		public double? Iобр_средневзв { get; set; }

		[Column]
		public double? Тист { get; set; }

		[Column]
		public double? Qпрям { get; set; }

		[Column]
		public double? Qобр1 { get; set; }

		[Column]
		public double? Qобр2 { get; set; }

		[Column]
		public double? Энтальпия_прямой { get; set; }

		[Column]
		public double? Энтальпия_обратной1 { get; set; }

		[Column]
		public double? Энтальпия_обратной2 { get; set; }

		[Column]
		public double? Поправка_к_Тпр_ { get; set; }

		[Column]
		public double? Поправка_к_Тобр1 { get; set; }

		[Column]
		public double? Поправка_к_Тобр2 { get; set; }

		[Column]
		public double? Разность { get; set; }

		[Column]
		public double? xGпрям { get; set; }

		[Column]
		public double? IТпрям { get; set; }

		[Column]
		public double? xGобр1 { get; set; }

		[Column]
		public double? IТобр1 { get; set; }

		[Column]
		public double? xGобр2 { get; set; }

		[Column]
		public double? IТобр2 { get; set; }

		[Column]
		public double? xIобр_средневзв { get; set; }

		[Column]
		public double? xQ { get; set; }

	}
}