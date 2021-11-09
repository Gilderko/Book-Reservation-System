using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.FullVersions
{
    public class EBookInstanceDTO : BaseEntityDTO
    {
        [Required]
        public int EBookTemplateID { get; set; }

        public EBookDTO FromBookTemplate { get; set; }

        [Required]
        public int EReaderID { get; set; }

        public EReaderInstanceDTO EReaderPlace { get; set; }
    }
}
