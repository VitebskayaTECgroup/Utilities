using LinqToDB;
using LinqToDB.Data;

namespace MnemonicaBase.Database
{
	public class DatabaseContext : DataConnection
	{
		public DatabaseContext() : base("Default") { }

		public ITable<DbTag> Tags
			=> this.GetTable<DbTag>();

		public ITable<DbTagLive> Live
			=> this.GetTable<DbTagLive>();

		public ITable<DbTagHistory> History
			=> this.GetTable<DbTagHistory>();
	}
}