using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.FullVersions
{
    public class EReaderDTO : BaseEntityDTO
    {
        [Required]
        [StringLength(64)]
        public string Model { get; set; }

        [Required]
        [StringLength(64)]
        public string CompanyMake { get; set; }

        [Required]
        public int MemoryInMB { get; set; }
    }
}
