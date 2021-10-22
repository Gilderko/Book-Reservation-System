using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Previews
{
    public class BookCollectionPrevDTO : BaseEntityDTO
    {
        [StringLength(128)]
        public string Title { get; set; }

        [StringLength(256)]
        public string Description { get; set; }
    }
}
