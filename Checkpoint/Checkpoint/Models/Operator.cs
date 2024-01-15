namespace Checkpoint.Models
{
    public class Operator
    {
        public int Id { get; set; }

        public string TableId { get; set; }

        public string Official { get; set; }

        public int? DepartmentId { get; set; }

        public string Description { get; set; }

        public bool IsAdmin => Description == "Operator";

        public bool IsCRUD { get; set; }
    }
}