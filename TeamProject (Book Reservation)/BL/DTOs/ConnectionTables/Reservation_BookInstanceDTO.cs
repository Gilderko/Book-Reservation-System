using BL.DTOs;
using BL.DTOs.FullVersions;

namespace DAL.Entities.ConnectionTables
{
    public class Reservation_BookInstanceDTO : IEntityDTO
    {
        public int ReservationID { get; set; }


        public ReservationDTO Reservation;

        public int BookInstanceID { get; set; }


        public BookInstanceDTO BookInstance;
    }
}
