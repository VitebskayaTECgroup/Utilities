using System;
using System.Configuration;
using System.Web.Mvc;

namespace Calc
{
	public static class Extensions
	{
		public static string Pretty(this double? value, int round = 4)
		{
			return value.HasValue ? Math.Round(value.Value, round).ToString().Replace(',', '.') : null;
		}

		public static double? AsMass(this double? value, int round = 0)
		{
			return value.HasValue ? Math.Round(value.Value, round) : value;
		}

		public static double? AsTemperature(this double? value, int round = 2)
		{
			return value.HasValue ? Math.Round(value.Value, round) : value;
		}

		public static double? AsHeat(this double? value, int round = 0)
		{
			return value.HasValue ? Math.Round(value.Value / 4.187, round) : value;
		}

		public static double? AsPressure(this double? value, int round = 2)
		{
			return value.HasValue ? Math.Round(value.Value, round) : value;
		}

		public static string Calc(this Controller controller)
		{
			return ConfigurationManager.ConnectionStrings["ptocalc"].ConnectionString;
		}

		public static string Runtime(this Controller controller)
		{
			return ConfigurationManager.ConnectionStrings["runtime"].ConnectionString;
		}
	}
}