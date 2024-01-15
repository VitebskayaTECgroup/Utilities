using LinqToDB.Mapping;
using System;
using System.Linq;

namespace ScanLan.Models
{
    [Table(Name = "NetworkLogs")]
    public class NetworkLog
    {
        [Column]
        public DateTime Date { get; set; }

        [Column]
        public string Username { get; set; }

        [Column]
        public string Text { get; set; }

        public string GetIp()
        {
            int i = Text.IndexOf("IP[");
            if (i < 0) return null;

            string ip = Text.Substring(i);
            i = ip.IndexOf(']');

            ip = ip.Substring(3, i - 3);
            return ip;
        }

        public string[] GetChanges()
        {
            return Text
                 .Replace("NAME[", "Наименование[")
                 .Replace("DESC[", "Описание[")
                 .Replace("LOGIN[", "Логин[")
                 .Replace("PASS[", "Пароль[")
                 .Replace("[", ": было ")
                 .Replace("|", ", стало ")
                 .Split(']')
                 .Where(x => !x.Contains("IP: было "))
                 .ToArray();
        }
    }
}