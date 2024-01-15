using LinqToDB.Mapping;
using System;

namespace MnemonicaBase.Database
{
	[Table(Name = "TagsLive")]
	public class DbTagLive
	{
		[Column]
		public string TagName { get; set; }

		[Column]
		public string Value { get; set; }

		[Column]
		public DateTime? Date { get; set; } = DateTime.Now;
	}
}