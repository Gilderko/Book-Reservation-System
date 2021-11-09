using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.FullVersions
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
        public string HashedPassword { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        public ICollection<BookCollectionDTO> BookCollections { get; set; }

        public ICollection<ReservationDTO> Reservations { get; set; }

        public ICollection<ReviewDTO> Reviews { get; set; }
    }
}
