using LinqToDB.Mapping;

namespace Checkpoint.Database
{
    /// <summary>
    /// Описания разрешений операторов приложения
    /// </summary>

    [Table]
    public class OPER_RIGHTS_DESC
    {
        [Column, Identity]
        public int OPRD_ID { get; set; }

        [Column]
        public string OPRD_RT_CODE { get; set; }

        [Column]
        public string OPRD_DESC { get; set; }

        [Column]
        public int OPRD_APP_TYPE { get; set; }
    }
}