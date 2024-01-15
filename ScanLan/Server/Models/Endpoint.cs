using System;

namespace ScanLan.Models
{
	public class Endpoint
	{
		public string Ip { get; set; }

		public DateTime? LastUpdate { get; set; }

		public string DnsName { get; set; }

		public string MacAddress { get; set; }

		public string Desc { get; set; }
	}
}