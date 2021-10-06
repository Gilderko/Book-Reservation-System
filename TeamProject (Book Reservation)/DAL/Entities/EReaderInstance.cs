using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class EReaderInstance : BaseEntity
    {
        public int EReaderTemplateID { get; set; }

        [ForeignKey(nameof(EReaderTemplateID))]
        public EReader EReaderTemplate { get; set; }

        public ICollection<EBookInstance> BooksIncluded { get; set; }
    }
}
