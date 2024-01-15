using ScanLan.Models;
using System.Collections.Generic;

namespace Client.Models
{
    public class Cache
    {
        public List<Switch> Switches { get; set; } = new List<Switch>();

        public List<Link> Links { get; set; } = new List<Link>();
    }
}