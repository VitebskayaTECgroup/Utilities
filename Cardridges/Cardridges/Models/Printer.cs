using LinqToDB.Mapping;

namespace Cardridges.Models
{
    [Table(Name = "Printers")]
    public class Printer
    {
        [Column, Identity]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }
    }
}