using LinqToDB.Mapping;
using System;

namespace ScanLan.Models
{
    [Table(Name = "Network")]
    public class NetworkEndpoint
    {
        [Column]
        public string Ip { get; set; }

        [Column]
        public DateTime? LastUpdate { get; set; }

        [Column]
        public string DnsName { get; set; }

        [Column]
        public string MacAddress { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Description { get; set; }

        [Column]
        public string Login { get; set; }

        [Column]
        public string Password { get; set; }

        public string Status()
        {
            if (!LastUpdate.HasValue) return "unseen";
            var hours = (DateTime.Now - LastUpdate.Value).TotalHours;
            if (hours > 24) return "unseen";
            if (hours > 1) return "off";
            return "on";
        }
    }
}
