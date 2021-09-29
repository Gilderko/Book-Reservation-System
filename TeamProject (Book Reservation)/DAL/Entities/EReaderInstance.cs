using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class EReaderInstance : BaseEntity
    {
        public int EReaderTemplateID { get; set; }

        [ForeignKey(nameof(EReaderTemplateID))]
        public EReaderTemplate EReaderTemplate { get; set; }

        public ICollection<EBookInstance> BooksIncluded { get; set; }
    }
}
