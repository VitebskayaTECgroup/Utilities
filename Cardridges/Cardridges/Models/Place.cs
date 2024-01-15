using LinqToDB.Mapping;

namespace Cardridges.Models
{
    [Table(Name = "WorkPlaces")]
    public class WorkPlace
    {
        [Column, Identity]
        public int Id { get; set; }

        [Column]
        public string Location { get; set; }

        [Column]
        public string Guild { get; set; }
    }
}