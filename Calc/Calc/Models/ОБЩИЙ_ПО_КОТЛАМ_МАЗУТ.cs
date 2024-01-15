using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "_ОБЩИЙ_ПО_КОТЛАМ_МАЗУТ_")]
	public class ОБЩИЙ_ПО_КОТЛАМ_МАЗУТ
	{
		[Column]
		public DateTime DateRec { get; set; }

		[Column]
		public double? Gпар { get; set; }

		[Column]
		public double? Gм_пар { get; set; }

		[Column]
		public double? Qпар { get; set; }

		[Column]
		public double? Qм_пар { get; set; }

		[Column]
		public double? Gкон { get; set; }

		[Column]
		public double? Gм_кон { get; set; }

		[Column]
		public double? Qкон { get; set; }

		[Column]
		public double? Qм_кон { get; set; }

		[Column]
		public double? Qпол { get; set; }

		[Column]
		public double? Qм_пол { get; set; }

		[Column]
		public double? Qгв { get; set; }

		[Column]
		public double? Qм_гв { get; set; }

		[Column]
		public double? Qобщ { get; set; }

		[Column]
		public double? Qм_общ { get; set; }

		[Column]
		public double? tmp { get; set; }

	}
}