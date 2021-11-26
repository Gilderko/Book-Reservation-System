using BL.DTOs.Entities.User;
using System;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.Reservation
{
    public class ReservationPrevDTO : BaseEntityDTO
    {
        public DateTime DateFrom { get; set; }

        public DateTime DateTill { get; set; }

        [Required]
        public int UserID { get; set; }

        public UserPrevDTO User { get; set; }
    }
}
