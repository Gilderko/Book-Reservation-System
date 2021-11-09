using System;
using System.Collections.Generic;
using DAL.Entities;

namespace BL.DTOs.Previews
{
    public class ReservationPrevDTO : BaseEntityDTO
    {
        public int Id { get; set; }
        public DateTime DateFrom { get; set; }

        public DateTime DateTill { get; set; }
    }
}
