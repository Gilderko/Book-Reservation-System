using System;

namespace BL.DTOs.Entities.Reservation
{
    public class ReservationPrevDTO : BaseEntityDTO
    {
        public DateTime DateFrom { get; set; }

        public DateTime DateTill { get; set; }
    }
}
