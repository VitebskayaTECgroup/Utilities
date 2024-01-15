using LinqToDB;
using LinqToDB.Data;

namespace Cardridges.Models
{
    public class DatabaseContext : DataConnection
    {
        public DatabaseContext() : base("Devin") { }

        public ITable<Device> Devices
            => GetTable<Device>();

        public ITable<Printer> Printers
            => GetTable<Printer>();

        public ITable<WorkPlace> WorkPlaces
            => GetTable<WorkPlace>();

        public ITable<UserRequest> UsersRequests
            => GetTable<UserRequest>();
    }

    public class SiteContext : DataConnection
    {
        public SiteContext() : base("Site") { }

        public ITable<User> Users
            => GetTable<User>();

        public ITable<Notification> Notifications
            => GetTable<Notification>();
    }
}