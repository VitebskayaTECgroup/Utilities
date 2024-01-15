using System;

namespace Checkpoint.Models
{
    public class Event
    {
        public int Id { get; set; }

        public int Type { get; set; }

        public int Code { get; set; }

        public DateTime Date { get; set; }

        public int OwnerId { get; set; }

        public int CardValue { get; set; }

        public static string FromType(int type, int code = 0)
        {
            if (code == 340) return "Алкотестирование запущено";
            if (code == 341) return "Алкотестирование, отказ от проверки, запрет";
            if (code == 342) return "Алкотестирование, найден алкоголь, запрет";
            if (code == 343) return "Алкотестирование успешно пройдено";
            if (type == 1) return "Вход 1";
            if (type == 2) return "Выход 1";
            if (type == 1001) return "Вход 2";
            if (type == 1002) return "Выход 2";
            return "Не определено";
        }
    }
}