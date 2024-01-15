using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "_ТГ2_")]
	public class ТГ2
	{
		[Column]
		public DateTime DateRec { get; set; }

		[Column]
		public double? TвремяП { get; set; }

		[Column]
		public double? PП { get; set; }

		[Column]
		public double? tП { get; set; }

		[Column]
		public double? GП { get; set; }

		[Column]
		public double? GП_ { get; set; }

		[Column]
		public double? Gпр1 { get; set; }

		[Column]
		public double? Gпр2 { get; set; }

		[Column]
		public double? GпрС { get; set; }

		[Column]
		public double? GпрС_ { get; set; }

		[Column]
		public double? Pпрп { get; set; }

		[Column]
		public double? tпрп { get; set; }

		[Column]
		public double? Gпм { get; set; }

		[Column]
		public double? Gкб { get; set; }

		[Column]
		public double? Pтеп { get; set; }

		[Column]
		public double? tтеп { get; set; }

		[Column]
		public double? tотр { get; set; }

		[Column]
		public double? Gкон { get; set; }

		[Column]
		public double? tпв { get; set; }

		[Column]
		public double? Gпв { get; set; }

		[Column]
		public double? Gпв_ { get; set; }

		[Column]
		public double? tвх { get; set; }

		[Column]
		public double? tвых { get; set; }

		[Column]
		public double? Рвкм { get; set; }

	}
}