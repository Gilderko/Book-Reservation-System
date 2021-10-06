using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class EBookInstance : BaseEntity
    {
        public int EBookTemplateID { get; set; }

        [ForeignKey(nameof(EBookTemplateID))]
        public EBook FromBookTemplate { get; set; }

        public int EReaderID { get; set; }

        [ForeignKey(nameof(EReaderID))]
        public EReaderInstance EReaderPlace { get; set; }
    }
}
