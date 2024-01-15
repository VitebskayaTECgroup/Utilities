using LinqToDB.Mapping;

namespace Checkpoint.Database
{
    /// <summary>
    /// Цеха и подразделения филиала
    /// </summary>

    [Table]
    public class OWNER_DEPS
    {
        [Column, Identity]
        public int OD_ID { get; set; }

        [Column]
        public string OD_NAME { get; set; }

        [Column]
        public string OD_DESC { get; set; }

        [Column]
        public int? OD_DEF_GROUP { get; set; }

        [Column]
        public string OD_EXT_ID { get; set; }
    }
}