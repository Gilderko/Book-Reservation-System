using BL.DTOs.Entities.User;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.Reservation
{
    public class ReservationPrevDTO : BaseEntityDTO
    {
        [DisplayName("From")]
        public DateTime DateFrom { get; set; }

        [DisplayName("To")]
        public DateTime DateTill { get; set; }

        [Required]
        public int UserID { get; set; }

        public UserPrevDTO User { get; set; }
    }
}
