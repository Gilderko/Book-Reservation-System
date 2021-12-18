using BL.DTOs.ConnectionTables;
using BL.DTOs.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.Book
{
    public class BookPrevDTO : BaseEntityDTO
    {
        [StringLength(64)]
        public string Title { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }

        public LanguageDTO Language { get; set; }

        // In map config add a custom mapping from Entity

        public ICollection<AuthorBookDTO> Authors { get; set; }
    }
}
