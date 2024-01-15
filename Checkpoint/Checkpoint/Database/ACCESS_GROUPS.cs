using LinqToDB.Mapping;

namespace Checkpoint.Database
{
    /// <summary>
    /// Группы пользователей, регистрируемых на проходной
    /// </summary>

    [Table]
    public class ACCESS_GROUPS
    {
        [Column, Identity]
        public int AGR_ID { get; set; }

        [Column]
        public string AGR_NAME { get; set; }
    }
}