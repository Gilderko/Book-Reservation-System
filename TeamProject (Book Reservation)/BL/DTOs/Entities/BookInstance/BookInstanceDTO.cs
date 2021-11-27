using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.User;
using BL.DTOs.Enums;
using DAL.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        public BookDTO FromBookTemplate { get; set; }

        [JsonIgnore]
        public ICollection<ReservationBookInstanceDTO> AllReservations { get; set; }
    }
}
