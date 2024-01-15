using LinqToDB.Mapping;

namespace Checkpoint.Database
{
    /// <summary>
    /// Описания должностей работников станции
    /// </summary>

    [Table]
    public class OWNER_JOBS
    {
        [Column, Identity]
        public int OJ_ID { get; set; }

        [Column]
        public string OJ_NAME { get; set; }
    }
}