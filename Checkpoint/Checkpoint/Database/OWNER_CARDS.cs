using LinqToDB.Mapping;
using System;

namespace Checkpoint.Database
{
    /// <summary>
    /// Список зарегистрированных карт пропусков
    /// </summary>

    [Table]
    public class OWNER_CARDS
    {
        [Column, Identity]
        public int OC_ID { get; set; }

        [Column]
        public int OC_OW_ID { get; set; }

        [Column]
        public int OC_TYPE { get; set; }

        [Column]
        public DateTime? OC_RETDATE { get; set; }

        [Column]
        public int OC_INACTIVE { get; set; }

        [Column]
        public int OC_VALUE { get; set; }

        [Column]
        public int OC_ARCHIVE { get; set; }

        [Column]
        public int? OC_ACCESS_GROUP { get; set; }
    }
}