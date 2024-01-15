using Aggregator.Models;
using Dapper;
using System;
using System.IO;

namespace Aggregator.Scripts
{
    public class Script_Gas
    {
        public static void Execute(int serverIndex)
        {
            try
            {
                Console.WriteLine("SCRIPT GAS");

                using (var runtime = new RuntimeContext(serverIndex))
                {
                    runtime.Connection.Execute("EXEC Runtime.dbo.vst_SET_CALC_HOUR;");
                }

                Console.WriteLine("SCRIPT GAS COMPLETE");
            }
            catch (Exception e)
            {
                File.AppendAllText(AppContext.BaseDirectory + "err.log", DateTime.Now + "\n" + e.Message + "\n" + e.StackTrace);
            }
        }
    }
}