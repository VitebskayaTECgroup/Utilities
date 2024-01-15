using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "_ОБЩИЙ_ПО_КОТЛАМ_ГАЗ_")]
	public class ОБЩИЙ_ПО_КОТЛАМ_ГАЗ
	{
		[Column]
		public DateTime DateRec { get; set; }

		[Column]
		public double? Gпар { get; set; }

	}
}