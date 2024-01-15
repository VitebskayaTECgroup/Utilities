using LinqToDB.Mapping;

namespace Checkpoint.Database
{
    /// <summary>
    /// Описания типов событий на проходной
    /// </summary>

    [Table]
    public class EVENT_DESC
    {
        [Column, Identity]
        public int ED_ID { get; set; }

        [Column]
        public int ED_CODE { get; set; }

        [Column]
        public int ED_TYPE { get; set; }

        [Column]
        public string ED_NAME { get; set; }
    }
}