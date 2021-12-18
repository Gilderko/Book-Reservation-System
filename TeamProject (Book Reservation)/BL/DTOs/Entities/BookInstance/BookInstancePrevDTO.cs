using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.User;
using BL.DTOs.Enums;
using System.ComponentModel;

namespace BL.DTOs.Entities.BookInstance
{
    public class BookInstancePrevDTO : BaseEntityDTO
    {
        public BookInstanceConditionDTO Condition { get; set; }

        public UserDTO Owner { get; set; }

        [DisplayName("Book")]
        public BookDTO FromBookTemplate { get; set; }
    }
}
