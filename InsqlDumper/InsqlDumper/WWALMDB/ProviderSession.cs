using LinqToDB.Mapping;

namespace InsqlDumper.WWALMDB
{
	[Table(Name = "ProviderSession")]
	public class ProviderSession
	{
		[Column, Identity] public int ProviderId { get; set; }
		[Column] public string NodeName { get; set; }
		[Column] public string ProviderType { get; set; }
		[Column] public string ApplicationName { get; set; }
		[Column] public string ApplicationVersion { get; set; }
		[Column] public int? ApplicationInstance { get; set; }
	}
}
