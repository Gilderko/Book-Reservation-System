using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.EReader;
using BL.DTOs.Entities.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.EReaderInstance
{
    public class EReaderInstanceDTO : BaseEntityDTO
    {
        [Required]
        [StringLength(1024)]
        public string Description { get; set; }

        [Required]
        public int EReaderTemplateID { get; set; }

        public int EreaderOwnerId { get; set; }

        [Required]
        public UserDTO Owner { get; set; }

        public EReaderDTO EReaderTemplate { get; set; }

        public ICollection<EBookEReaderInstanceDTO> BooksIncluded { get; set; }
    }
}
