using LinqToDB.Mapping;
using System;

namespace Cardridges.Models
{
    [Table(Name = "UsersRequests")]
    public class UserRequest
    {
        [Column, Identity]
        public int Id { get; set; }

        [Column]
        public string Win { get; set; }

        [Column]
        public int DeviceId { get; set; }

        [Column]
        public string Status { get; set; }

        [Column]
        public string Comment { get; set; }

        [Column]
        public DateTime Date { get; set; }

        [Column]
        public string ReviewUser { get; set; }
    }
}