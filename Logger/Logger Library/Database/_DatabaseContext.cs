using LinqToDB;
using LinqToDB.Data;

namespace Logger_Library.Database
{
	public class DatabaseContext : DataConnection
	{
		public DatabaseContext(): base("Default") {}

		public ITable<Agent> Agents
			=> this.GetTable<Agent>();

		public ITable<Configuration> Configurations
			=> this.GetTable<Configuration>();

		public ITable<EventFilter> EventFilters
			=> this.GetTable<EventFilter>();

		public ITable<Log> Logs
			=> this.GetTable<Log>();

		public ITable<Specification> Specifications
			=> this.GetTable<Specification>();

		public ITable<SpecSettings> SpecSettings
			=> this.GetTable<SpecSettings>();
	}
}
