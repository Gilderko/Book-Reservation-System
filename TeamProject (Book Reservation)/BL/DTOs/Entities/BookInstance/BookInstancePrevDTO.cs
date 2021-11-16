using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.User;
using DAL.Enums;

namespace BL.DTOs.Entities.BookInstance
{
    public class BookInstancePrevDTO : BaseEntityDTO
    {
        public BookInstanceCondition Conditon { get; set; }

        public UserDTO Owner { get; set; }

        public BookDTO FromBookTemplate { get; set; }
    }
}
