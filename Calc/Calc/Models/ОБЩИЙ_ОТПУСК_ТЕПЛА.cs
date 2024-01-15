using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "_ОБЩИЙ_ОТПУСК_ТЕПЛА_")]
	public class ОБЩИЙ_ОТПУСК_ТЕПЛА
	{
		[Column]
		public DateTime DateRec { get; set; }

		[Column]
		public double? tmp { get; set; }

	}
}