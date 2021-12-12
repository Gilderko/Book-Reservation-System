using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.EReader
{
    public class EReaderDTO : BaseEntityDTO
    {
        [Required]
        [StringLength(64)]
        public string Model { get; set; }

        [Required]
        [StringLength(64)]
        [Display(Name = "Manufacturer")]
        public string CompanyMake { get; set; }

        [Required]
        [Display(Name = "Memory size")]
        public int MemoryInMB { get; set; }
    }
}
