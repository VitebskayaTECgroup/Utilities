using LinqToDB;
using LinqToDB.Data;

namespace ScanLan.Models
{
    public class DatabaseContext : DataConnection
    {
        public DatabaseContext() : base("Everest") { }

        public ITable<Switch> Switches
            => GetTable<Switch>();

        public ITable<SwitchPortMac> SwitchesScheme
            => GetTable<SwitchPortMac>();

        public ITable<NetworkEndpoint> Network
            => GetTable<NetworkEndpoint>();

        public ITable<NetworkLog> Logs
            => GetTable<NetworkLog>();
    }
}