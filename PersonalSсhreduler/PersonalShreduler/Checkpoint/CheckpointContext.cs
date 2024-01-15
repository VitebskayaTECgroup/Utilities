using LinqToDB;
using LinqToDB.Data;

namespace PersonalShreduler.Checkpoint
{
	public class CheckpointContext : DataConnection
	{
		public CheckpointContext() : base("Checkpoint") { }

		public ITable<OWNERS> OWNERS
			=> GetTable<OWNERS>();

		public ITable<EVENTS> EVENTS
			=> GetTable<EVENTS>();
	}
}