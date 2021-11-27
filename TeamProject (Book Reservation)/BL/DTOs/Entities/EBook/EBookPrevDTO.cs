using BL.DTOs.Entities.Book;
using BL.DTOs.Enums;
using DAL.Enums;

namespace BL.DTOs.Entities.EBook
{
    public class EBookPrevDTO : BookPrevDTO
    {
        public EBookFormatDTO Format { get; set; }
    }
}