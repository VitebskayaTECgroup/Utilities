using LinqToDB;
using LinqToDB.Data;

namespace Aggregator.Models
{
	public class CalcContext : DataConnection
	{
		public CalcContext(int serverIndex = 2) : base("Calc" + serverIndex) { }

		public ITable<KA_Fuel> KA_Fuel
			=> this.GetTable<KA_Fuel>();

		public ITable<KA3> KA3
			=> this.GetTable<KA3>();

		public ITable<KA5> KA5
			=> this.GetTable<KA5>();
	}
}