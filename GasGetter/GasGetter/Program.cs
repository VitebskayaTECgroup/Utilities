using Dapper;
using System;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GasGetter
{
	class Program
	{
		static void Main(string[] args)
		{
			float[] val = new float[2] { 0, 0 };
			bool[] flags = new bool[2] { true, true };

			Task.WaitAll(new Task[]
			{
				Task.Run(() =>
				{
					try
					{
						using (var db = new OdbcConnection("Driver={SQL Server}; Server=INSQL1; Database=Runtime; Uid=wwAdmin; Pwd=wwAdmin;"))
						{
							val[0] = db.QueryFirst<float>("EXEC [dbo].[vst_GetGasValue]");
						}

						Console.WriteLine("Get from INSQL1");
					}
					catch (Exception e) 
					{
						Console.WriteLine("Error INSQL1: " + e.Message);
						flags[0] = false;
					}
				}),
				Task.Run(() =>
				{
					try
					{
						using (var db = new OdbcConnection("Driver={SQL Server}; Server=INSQL2; Database=Runtime; Uid=wwAdmin; Pwd=wwAdmin;"))
						{
							val[1] = db.QueryFirst<float>("EXEC [dbo].[vst_GetGasValue]");
						}

						Console.WriteLine("Get from INSQL2");
					}
					catch (Exception e) 
					{
						Console.WriteLine("Error INSQL2: " + e.Message);
						flags[1] = false;
					}
				})
			});

			try
			{
				using (var db = new SqlConnection(@"Data Source=WEB\SQLEXPRESS; Initial Catalog=Site; Persist Security Info=True; User ID=Site; Password=Site"))
				{
					db.Execute("UPDATE [Constants] SET [Value] = @Value WHERE Keyword = 'GasValue'", new { Value = Math.Max(val[0], val[1]) });
					db.Execute("UPDATE [Constants] SET [Value] = @Value WHERE Keyword = 'GasInsql1'", new { Value = flags[0] });
					db.Execute("UPDATE [Constants] SET [Value] = @Value WHERE Keyword = 'GasInsql2'", new { Value = flags[1] });
				}

				Console.WriteLine("Write to WEB");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error WEB: " + e.Message);
			}
		}
	}
}
