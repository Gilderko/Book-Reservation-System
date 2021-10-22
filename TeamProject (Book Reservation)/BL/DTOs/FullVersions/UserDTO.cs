using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.FullVersions
{
    public class UserDTO : BaseEntityDTO
    {
        [StringLength(32)]
        public string Name { get; set; }

        [StringLength(32)]
        public string Surname { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(256)]
        public string HashedPassword { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<BookCollectionDTO> BookCollections { get; set; }

        public ICollection<ReservationDTO> Reservations { get; set; }

        public ICollection<ReviewDTO> Reviews { get; set; }
    }
}
