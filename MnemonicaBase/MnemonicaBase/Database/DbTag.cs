using LinqToDB.Mapping;

namespace MnemonicaBase.Database
{
	[Table(Name = "Tags")]
	public class DbTag
	{
		[Identity]
		public long Id { get; set; }

		[Column]
		public string TagName { get; set; }

		[Column]
		public string Description { get; set; }
	}
}