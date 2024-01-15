using System;
using System.Collections.Generic;
using System.IO;

namespace Checkpoint.Models
{
	public class WorkBoundaries
	{
		static string ExceptionsTxt = @"\\web\Files\Проходная\исключения.txt";

		static TimeSpan DayWorkStart() => TimeSpan.Parse("08:00:00");

		static TimeSpan DayDinnerStart() => TimeSpan.Parse("12:30:00");

		static TimeSpan DayDinnerEnd() => TimeSpan.Parse("13:15:00");

		static TimeSpan DayWorkEnd(DateTime date)
		{
			var exceptions = new Dictionary<DateTime, TimeSpan>();

			try
			{
				var txt = File.ReadAllLines(ExceptionsTxt);
				foreach (var line in txt)
				{
					var parts = line.Split('|');
					exceptions.Add(DateTime.Parse(parts[0].Trim()), TimeSpan.Parse(parts[1].Trim()));
				}
			}
			catch { }

			if (exceptions.TryGetValue(date, out var ex))
			{
				return ex;
			}
			else
			{
				return date.DayOfWeek == DayOfWeek.Friday && date.Year > 2022
					? TimeSpan.Parse("15:45:00")
					: TimeSpan.Parse("17:00:00");
			}
		}


		public WorkBoundaries(DateTime date)
		{
			WorkStart = DayWorkStart();
			DinnerStart = DayDinnerStart();
			DinnerEnd = DayDinnerEnd();
			WorkEnd = DayWorkEnd(date);
		}

		public TimeSpan WorkStart { get; set; }

		public TimeSpan DinnerStart { get; set; }

		public TimeSpan DinnerEnd { get; set; }

		public TimeSpan WorkEnd { get; set; }
	}
}