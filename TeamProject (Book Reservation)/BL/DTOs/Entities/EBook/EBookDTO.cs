using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Book;
using BL.DTOs.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.EBook
{
    public class EBookDTO : BookDTO
    {
        [Required]
        [DisplayName("Memory size")]
        public int MemorySize { get; set; }

        [Required]
        public EBookFormatDTO Format { get; set; }

        [DisplayName("E-Readers")]
        public ICollection<EBookEReaderInstanceDTO> EReaders { get; set; }
    }
}
