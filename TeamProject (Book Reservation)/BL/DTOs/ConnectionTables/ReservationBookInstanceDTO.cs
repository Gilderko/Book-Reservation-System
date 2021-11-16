using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.Reservation;

namespace BL.DTOs.ConnectionTables
{
    public class ReservationBookInstanceDTO : IEntityDTO
    {
        public int ReservationID { get; set; }

        public ReservationDTO Reservation { get; set; }

        public int BookInstanceID { get; set; }

        public BookInstanceDTO BookInstance { get; set; }
    }
}
