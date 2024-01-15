using LinqToDB.Mapping;
using System.Collections.Generic;

namespace ScanLan.Models
{
    [Table(Name = "Switches")]
    public class Switch
    {
        [Column]
        public string Ip { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Model { get; set; }

        [Column]
        public string Mac { get; set; }

        [Column]
        public string Commands { get; set; }

        [Column]
        public int X { get; set; }

        [Column]
        public int Y { get; set; }

        public string[] GetCommands() => Commands.Split('|');

        public List<Port> Ports { get; set; } = new List<Port>();

        public List<string> Output { get; set; } = new List<string>();
    }
}