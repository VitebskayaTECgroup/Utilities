using LinqToDB.Mapping;
using System;

namespace Calc.Models
{

	[Table(Name = "KA_Fuel")]
	public class KA_Fuel
	{
		[Column]
		public DateTime DateTime { get; set; }

		[Column]
		public decimal K1_AI1PV { get; set; }

		[Column]
		public decimal K1_AI2PV { get; set; }

		[Column]
		public decimal K1_AI3PV { get; set; }

		[Column]
		public decimal K2_AI1PV { get; set; }

		[Column]
		public decimal K2_AI2PV { get; set; }

		[Column]
		public decimal K2_AI3PV { get; set; }

		[Column]
		public decimal K3_AI1PV { get; set; }

		[Column]
		public decimal K3_AI2PV { get; set; }

		[Column]
		public decimal K3_AI3PV { get; set; }

		[Column]
		public decimal K3_AI4PV { get; set; }

		[Column]
		public decimal K4_AI1PV { get; set; }

		[Column]
		public decimal K4_AI2PV { get; set; }

		[Column]
		public decimal K4_AI3PV { get; set; }

		[Column]
		public decimal K4_AI4PV { get; set; }

		[Column]
		public decimal K5_AI1PV { get; set; }

		[Column]
		public decimal K5_AI2PV { get; set; }

		[Column]
		public decimal K5_AI3PV { get; set; }

		[Column]
		public decimal K5_AI4PV { get; set; }

	}
}