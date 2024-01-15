using LinqToDB.Mapping;

namespace Checkpoint.Database
{
    /// <summary>
    /// Разрешения операторов приложения
    /// </summary>

    [Table]
    public class OPER_RIGHTS
    {
        [Column, Identity]
        public int OPR_ID { get; set; }

        [Column]
        public int OPR_OP_ID { get; set; }

        [Column, NotNull]
        public string OPR_CODE { get; set; }

        [Column]
        public string OPR_DATA { get; set; }

        [Column]
        public int? OPR_REF { get; set; }
    }
}