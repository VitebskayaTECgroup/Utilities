using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "_сведениебаланса_")]
	public class СведениеБаланса
	{
		[Column]
		public DateTime DateRec { get; set; }

		[Column]
		public double? Мпр_центр { get; set; }

		[Column]
		public double? Моб_центр { get; set; }

		[Column]
		public double? Мпр_западная { get; set; }

		[Column]
		public double? Моб_западная { get; set; }

		[Column]
		public double? Мпр_лучеса { get; set; }

		[Column]
		public double? Моб_лучеса { get; set; }

		[Column]
		public double? Мпр_монолит { get; set; }

		[Column]
		public double? Моб_монолит { get; set; }

		[Column]
		public double? Мпр_док { get; set; }

		[Column]
		public double? Моб_док { get; set; }

		[Column]
		public double? Мнб_max { get; set; }

		[Column]
		public double? М_пот { get; set; }

		[Column]
		public double? Мпод { get; set; }

		[Column]
		public double? Кнеб { get; set; }

		[Column]
		public double? Мпр_попр_центр { get; set; }

		[Column]
		public double? Моб_попр_центр { get; set; }

		[Column]
		public double? Мпр_попр_западная { get; set; }

		[Column]
		public double? Моб_попр_западная { get; set; }

		[Column]
		public double? Мпр_попр_лучеса { get; set; }

		[Column]
		public double? Моб_попр_лучеса { get; set; }

		[Column]
		public double? Мпр_попр_монолит { get; set; }

		[Column]
		public double? Моб_попр_монолит { get; set; }

		[Column]
		public double? Мпр_попр_док { get; set; }

		[Column]
		public double? Моб_попр_док { get; set; }

		[Column]
		public double? Мпр_корр_центральная { get; set; }

		[Column]
		public double? Моб_корр_центральная { get; set; }

		[Column]
		public double? Мпр_корр_западная { get; set; }

		[Column]
		public double? Моб_корр_западная { get; set; }

		[Column]
		public double? Мпр_корр_лучеса { get; set; }

		[Column]
		public double? Моб_корр_лучеса { get; set; }

		[Column]
		public double? Мпр_корр_монолит { get; set; }

		[Column]
		public double? Моб_корр_монолит { get; set; }

		[Column]
		public double? Мпр_корр_док { get; set; }

		[Column]
		public double? Моб_корр_док { get; set; }

		[Column]
		public double? Моб1_корр_док { get; set; }

		[Column]
		public double? Моб2_корр_док { get; set; }

		[Column]
		public double? Мподп_центр { get; set; }

		[Column]
		public double? Мподп_запад { get; set; }

		[Column]
		public double? Мподп_лучеса { get; set; }

		[Column]
		public double? Мподп_монолит { get; set; }

		[Column]
		public double? Мподп_док { get; set; }

	}
}