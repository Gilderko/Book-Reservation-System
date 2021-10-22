using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.FullVersions
{
    public class EReaderDTO : BaseEntityDTO
    {
        [StringLength(64)]
        public string Model { get; set; }

        [StringLength(64)]
        public string CompanyMake { get; set; }

        public int MemoryInMB { get; set; }
    }
}
