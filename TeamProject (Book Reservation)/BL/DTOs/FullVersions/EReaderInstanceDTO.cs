using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.FullVersions
{
    public class EReaderInstanceDTO : BaseEntityDTO
    {
        [Required]
        public int EReaderTemplateID { get; set; }

        public int EreaderOwnerId { get; set; }

        [Required]
        public UserDTO Owner { get; set; }

        public EReaderDTO EReaderTemplate { get; set; }

        public ICollection<EBookInstanceDTO> BooksIncluded { get; set; }
    }
}
