using System.Collections.Generic;

namespace ScanLan.Models
{
	public class Switch
	{
		public string Ip { get; set; }

		public string Name { get; set; }

		public string Model { get; set; }

		public string Mac { get; set; }

		public string Commands { get; set; }

		public string[] GetCommands() => Commands.Split('|');

		public List<Port> Ports { get; set; } = new List<Port>();

		public List<string> Output { get; set; } = new List<string>();
	}
}