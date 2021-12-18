using System.ComponentModel;
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
        [DisplayName("Manufacturer")]
        public string CompanyMake { get; set; }

        [Required]
        [DisplayName("Memory size")]
        public int MemoryInMB { get; set; }
    }
}
