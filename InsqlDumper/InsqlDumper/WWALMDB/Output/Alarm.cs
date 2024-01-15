using System;

namespace InsqlDumper.WWALMDB.Output
{
	public class Alarm
	{
		public DateTime Date { get; set; }

		public string TagName { get; set; }

		public string Node { get; set; }

		public string Type { get; set; }

		public string Value { get; set; }

		public string ControlValue { get; set; }

		public string Comment { get; set; }

		public string Class { get; set; }
	}
}
