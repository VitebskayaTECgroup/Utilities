using LinqToDB.Mapping;
using System;

namespace MnemonicaBase.Database
{
	[Table(Name = "TagsHistory")]
	public class DbTagHistory
	{
		[Identity]
		public long Id { get; set; }

		[Column]
		public DateTime Date { get; set; }

		[Column]
		public string TagName { get; set; }

		[Column]
		public string Value { get; set; }
	}
}