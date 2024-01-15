using ScanLan.Modules;
using System;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace ScanLan
{
	public class Program
	{
		static void Main()
		{
			if (Environment.UserInteractive)
			{
				Start();
				Console.ReadLine();
				Stop();
			}
			else
			{
				using (var service = new Service())
				{
					ServiceBase.Run(service);
				}
			}
		}

		public static void Start()
		{
			Task.Run(() =>
			{
				while (true)
				{
					Scheme.Process();
					Task.Delay(TimeSpan.FromMinutes(1)).Wait();
				}
			});
		}

		public static void Stop()
		{
			
		}
	}

	public class Service : ServiceBase
	{
		public Service()
		{
			ServiceName = "ScanLan";
		}

		protected override void OnStart(string[] args)
		{
			Program.Start();
		}

		protected override void OnStop()
		{
			Program.Stop();
		}
	}
}