using System;
using System.Threading.Tasks;

namespace WeatherParser
{
	class Program
	{
		public static void Main()
		{
			Task.Run(Parser.MeteoByAsync).Wait();
			Uploader.Upload();

			#if DEBUG
			Console.WriteLine("Done!");
			Console.ReadLine();
			#endif
		}
	}
}