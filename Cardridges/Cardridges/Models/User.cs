using LinqToDB.Mapping;

namespace Cardridges.Models
{
    [Table(Name = "Users")]
    public class User
    {
        [Column, Identity]
        public int UID { get; set; }

        [Column]
        public string UName { get; set; }

        [Column]
        public string DisplayName { get; set; }

        [Column]
        public string UClass { get; set; }
    }
}