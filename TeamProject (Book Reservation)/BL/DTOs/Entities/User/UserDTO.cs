using BL.DTOs.Entities.BookCollection;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.EReaderInstance;
using BL.DTOs.Entities.Reservation;
using BL.DTOs.Entities.Review;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.User
{
    public class UserDTO : BaseEntityDTO
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [Required]
        [StringLength(32)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(256)]
        [DisplayName("Hashed password")]
        public string HashedPassword { get; set; }

        [Required]
        [DisplayName("Admin")]
        public bool IsAdmin { get; set; }

        [DisplayName("Book collections")]
        public ICollection<BookCollectionDTO> BookCollections { get; set; }

        public ICollection<ReservationDTO> Reservations { get; set; }

        public ICollection<ReviewDTO> Reviews { get; set; }

        [DisplayName("Books")]
        public ICollection<BookInstanceDTO> MyBooks { get; set; }

        [DisplayName("E-Readers")]
        public ICollection<EReaderInstanceDTO> MyEReaders { get; set; }
    }
}
