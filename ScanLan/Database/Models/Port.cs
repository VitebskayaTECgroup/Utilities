using System.Collections.Generic;

namespace ScanLan.Models
{
    public class Port
    {
        public string Name { get; set; }

        public int Type { get; set; } = 0;

        public LinkPart Link { get; set; }

        public List<Device> Devices { get; set; } = new List<Device>();
    }

    public enum PortType
    {
        Device,
        DevicesGroup,
        Switch,
        Connect
    }
}