using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.EReaderInstance
{
    public class EReaderInstanceCreateDTO : BaseEntityDTO
    {
        [Required]
        [StringLength(1024)]
        public string Description { get; set; }

        [Required]
        public int EReaderTemplateID { get; set; }

        public int EreaderOwnerId { get; set; }
    }
}
