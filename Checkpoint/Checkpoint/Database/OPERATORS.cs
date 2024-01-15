using LinqToDB.Mapping;

namespace Checkpoint.Database
{
    /// <summary>
    /// Операторы проходной, имеющие доступ к ее данным
    /// </summary>

    [Table]
    public class OPERATORS
    {
        [Column, Identity]
        public int OP_ID { get; set; }

        [Column]
        public string OP_NAME { get; set; }

        [Column]
        public string OP_LOGIN { get; set; }

        [Column]
        public string OP_DESC { get; set; }

        [Column]
        public int? OP_ARCHIVE { get; set; }

        [Column]
        public int? OP_OWNER { get; set; }

        [Column]
        public int OP_TYPE { get; set; } = 0;
    }
}