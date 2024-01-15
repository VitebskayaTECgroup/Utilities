using LinqToDB.Mapping;

namespace Cardridges.Models
{
    [Table(Name = "Devices")]
    public class Device
    {
        [Column, Identity]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Type { get; set; }

        [Column]
        public int ComputerId { get; set; }

        [Column]
        public int PrinterId { get; set; }

        [Column]
        public int PlaceId { get; set; }

        [Column]
        public bool IsOff { get; set; }

        [Column]
        public bool IsDeleted { get; set; }

        [Column]
        public string Users { get; set; }
    }
}