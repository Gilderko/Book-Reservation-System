using System;
using System.Collections.Generic;

namespace BL.DTOs.Previews
{
    public class ReservationPrevDTO : BaseEntityDTO
    {
        public DateTime DateFrom { get; set; }

        public DateTime DateTill { get; set; }

        // Many to many relationships

        public ICollection<BookPrevDTO> Book { get; set; }
    }
}
