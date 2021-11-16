using BL.DTOs.Entities.Book;
using DAL.Enums;

namespace BL.DTOs.Entities.EBook
{
    public class EBookPrevDTO : BookPrevDTO
    {
        public EBookFormat Format { get; set; }
    }
}