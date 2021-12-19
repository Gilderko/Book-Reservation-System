using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.User;
using BL.DTOs.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.BookInstance
{
    public class BookInstanceDTO : BaseEntityDTO
    {
        [Required]
        public BookInstanceConditionDTO Condition { get; set; }

        [Required]
        public int BookOwnerId { get; set; }

        public UserDTO Owner { get; set; }

        [Required]
        public int BookTemplateID { get; set; }

        [DisplayName("Book")]
        public BookDTO FromBookTemplate { get; set; }

        [DisplayName("Reservations")]
        public ICollection<ReservationBookInstanceDTO> AllReservations { get; set; }
    }
}
