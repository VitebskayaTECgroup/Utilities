using LinqToDB.Data;

namespace Aggregator.Models
{
	public class RuntimeContext : DataConnection
	{
		public RuntimeContext(int serverIndex = 2) : base("Runtime" + serverIndex) { }
	}
}