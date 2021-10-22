using System.Collections.Generic;

namespace BL.DTOs.FullVersions
{
    public class EReaderInstanceDTO : BaseEntityDTO
    {
        public int EReaderTemplateID { get; set; }

        public EReaderDTO EReaderTemplate { get; set; }

        public ICollection<EBookInstanceDTO> BooksIncluded { get; set; }
    }
}
