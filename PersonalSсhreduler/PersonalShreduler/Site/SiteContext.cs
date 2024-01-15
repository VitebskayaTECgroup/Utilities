using LinqToDB;
using LinqToDB.Data;

namespace PersonalShreduler.Site
{
	public class SiteContext : DataConnection
	{
		public SiteContext() : base("Site") { }

		public ITable<Personal> Personal
			=> GetTable<Personal>();
	}
}