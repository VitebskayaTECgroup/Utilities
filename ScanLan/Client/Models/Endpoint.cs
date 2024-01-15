using ScanLan.Models;

namespace Client.Models
{
    public class Endpoint : NetworkEndpoint
    {
        public string Port { get; set; }

        public Switch Switch { get; set; }
    }
}