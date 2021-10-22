using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Previews
{
    public class EReaderPrevDTO : BaseEntityDTO
    {
        [StringLength(64)]
        public string Model { get; set; }

        [StringLength(64)]
        public string CompanyMake { get; set; }
    }
}
