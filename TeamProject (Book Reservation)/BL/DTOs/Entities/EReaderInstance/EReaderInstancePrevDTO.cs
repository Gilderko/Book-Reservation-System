using BL.DTOs.Entities.EReader;
using System.ComponentModel;

namespace BL.DTOs.Entities.EReaderInstance
{
    public class EReaderInstancePrevDTO : BaseEntityDTO
    {
        public int EReaderTemplateID { get; set; }
        
        [DisplayName("E-Reader")]
        public EReaderDTO EReaderTemplate { get; set; }
        
        public string Description { get; set; }
    }
}