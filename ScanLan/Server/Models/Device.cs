using System;

namespace ScanLan.Models
{
	public class Device
	{
		public string Mac { get; set; }

		public DateTime Date { get; set; }

		public string Vlan { get; set; }

		public string Ip { get; set; }

		public string Desc { get; set; }

		public string Dns { get; set; }
	}
}