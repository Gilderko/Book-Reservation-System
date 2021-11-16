using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.BookCollection
{
    public class BookCollectionPrevDTO : BaseEntityDTO
    {
        [StringLength(128)]
        public string Title { get; set; }

        [StringLength(256)]
        public string Description { get; set; }
    }
}
