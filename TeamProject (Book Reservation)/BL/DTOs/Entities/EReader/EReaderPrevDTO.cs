using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.EReader
{
    public class EReaderPrevDTO : BaseEntityDTO
    {
        [StringLength(64)]
        public string Model { get; set; }

        [StringLength(64)]
        [DisplayName("Maufacturer")]
        public string CompanyMake { get; set; }
    }
}
