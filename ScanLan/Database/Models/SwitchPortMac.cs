using LinqToDB.Mapping;
using System;

namespace ScanLan.Models
{
    [Table(Name = "SwitchesScheme")]
    public class SwitchPortMac
    {
        [Column]
        public string SwitchIp { get; set; }

        [Column]
        public string PortName { get; set; }

        [Column]
        public string Mac { get; set; }

        [Column]
        public DateTime Date { get; set; }

        public bool Online() => (DateTime.Now - Date) < TimeSpan.FromMinutes(5);
    }
}