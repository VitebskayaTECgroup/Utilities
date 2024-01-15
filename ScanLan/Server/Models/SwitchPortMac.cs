using System;

namespace ScanLan.Models
{
	public class SwitchPortMac
	{
		public string SwitchIp { get; set; }

		public string PortName { get; set; }

		public string Mac { get; set; }

		public string Vlan { get; set; }

		public string Ip { get; set; }

		public string Dns { get; set; }

		public string Desc { get; set; }

		public DateTime Date { get; set; }
	}
}