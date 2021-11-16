using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities.ConnectionTables
{
    public class EBookEReaderInstance
    {
        public int EBookID { get; set; }

        [ForeignKey(nameof(EBookID))]
        public EBook EBook { get; set; }

        public int EReaderInstanceID { get; set; }

        [ForeignKey(nameof(EReaderInstanceID))]
        public EReaderInstance EReader { get; set; }
    }
}
