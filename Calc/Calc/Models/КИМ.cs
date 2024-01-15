using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "_КИМ_")]
	public class КИМ
	{
		[Column]
		public DateTime DateRec { get; set; }

		[Column]
		public double? PП { get; set; }

		[Column]
		public double? tП { get; set; }

		[Column]
		public double? tв { get; set; }

		[Column]
		public double? Qпот_пар { get; set; }

		[Column]
		public double? Qком_пар { get; set; }

		[Column]
		public double? Gком_пар { get; set; }

		[Column]
		public double? Qпот_конд { get; set; }

		[Column]
		public double? Qком_конд { get; set; }

		[Column]
		public double? Gком_конд { get; set; }

		[Column]
		public double? Qким_пол { get; set; }

	}
}