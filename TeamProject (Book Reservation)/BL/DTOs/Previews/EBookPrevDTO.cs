using DAL.Enums;

namespace BL.DTOs.Previews
{
    public class EBookPrevDTO : BookPrevDTO
    {
        public EBookFormat Format { get; set; }
    }
}