using BL.DTOs.Entities.EReader;

namespace BL.DTOs.Entities.EReaderInstance
{
    public class EReaderInstancePrevDTO : BaseEntityDTO
    {
        public int EReaderTemplateID { get; set; }
        public EReaderDTO EReaderTemplate { get; set; }
        public string Description { get; set; }
    }
}