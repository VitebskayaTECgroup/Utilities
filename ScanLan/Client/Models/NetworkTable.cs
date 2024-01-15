using ScanLan.Models;
using System.Collections.Generic;

namespace Client.Models
{
	public class NetworkTable
	{
		public string NetworkMask { get; set; }

		public List<NetworkEndpoint> Endpoints { get; set; } = new List<NetworkEndpoint>();
	}
}