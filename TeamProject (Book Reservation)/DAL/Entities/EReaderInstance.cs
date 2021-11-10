using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class EReaderInstance : BaseEntity
    {
        [MaxLength(1024)]
        public string Description { get; set; }

        public int EreaderOwnerId { get; set; }

        [ForeignKey(nameof(EreaderOwnerId))]
        public User Owner { get; set; }

        public int EReaderTemplateID { get; set; }

        [ForeignKey(nameof(EReaderTemplateID))]
        public EReader EReaderTemplate { get; set; }

        public ICollection<EBookInstance> BooksIncluded { get; set; }
    }
}
