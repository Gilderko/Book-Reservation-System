using DAL.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Previews
{
    public class BookPrevDTO : BaseEntityDTO
    {
        [StringLength(64)]
        public string Title { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }

        public Language Language { get; set; }

        // In map config add a custom mapping from Entity

        public ICollection<AuthorPrevDTO> Authors { get; set; }
    }
}
