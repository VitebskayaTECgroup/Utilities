using System;

namespace ScanLan.Models
{
    public class Device
    {
        public string Mac { get; set; }

        public DateTime Date { get; set; }

        public bool Online { get; set; }

        public string Ip { get; set; }

        public string Dns { get; set; }
    }
}