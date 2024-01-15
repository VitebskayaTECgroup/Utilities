using System;
using System.Threading.Tasks;

namespace Aggregator
{
	class Program
	{
		static void Main()
		{
			for (int i = 1; i < 3; i++)
			{
				Scripts.Script_KA_Fuel.Execute(i);
				Scripts.Script_KA3.Execute(i);
				Scripts.Script_KA5.Execute(i);
			}
			Scripts.Script_Gas.Execute(2);
			Console.WriteLine("ALL SCRIPTS DONE");
			Task.Delay(2000).Wait();
		}
	}
}