using System;

namespace Checkpoint.Models
{
    public class Timeline
    {
        public int Id { get; set; }
        
        public DateTime Date { get; set; }
        
        public string Type { get; set; }

        public int InitialType { get; set; }

        public string Direction { get; set; }

        public string Official { get; set; }

        public string Group { get; set; }

        public string Department { get; set; }
    }
}