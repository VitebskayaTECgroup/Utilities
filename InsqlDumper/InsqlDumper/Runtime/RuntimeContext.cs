using InsqlDumper.Runtime.Output;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InsqlDumper.Database
{
	public class RuntimeContext : DataConnection
	{
		public RuntimeContext() : base("Runtime")
		{
			this.CommandTimeout = 240;
		}

		public ITable<IOServer> IOServer
			=> this.GetTable<IOServer>();

		public ITable<IODriver> IODriver
			=> this.GetTable<IODriver>();

		public ITable<QualityMap> QualityMap
			=> this.GetTable<QualityMap>();

		public ITable<StorageNode> StorageNode
			=> this.GetTable<StorageNode>();

		public ITable<Tag> Tag
			=> this.GetTable<Tag>();

		public ITable<Topic> Topic
			=> this.GetTable<Topic>();

		public ITable<V_History> History
			=> this.GetTable<V_History>();

		public List<TagValue> DumpTag(string tag, DateTime start, DateTime end, bool delta = true, int resolution = 1000)
		{
			try
			{
				return History
					.Where(x => x.TagName == tag)
					.Where(x => x.DateTime >= start)
					.Where(x => x.DateTime < end)
					.Where(x => x.WwRetrievalMode == (delta ? "DELTA" : "CYCLIC"))
					.Where(x => delta || x.WwResolution == resolution)
					.Where(x => x.WwVersion == "LATEST")
					.OrderBy(x => x.DateTime)
					.Select(x => new TagValue
					{
						DateTime = x.DateTime.ToString("yyyy.MM.dd HH:mm:ss.fff"),
						TagName = x.TagName,
						Value = x.VValue,
					})
					.ToList();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return new List<TagValue>();
			}
		}
	}
}
