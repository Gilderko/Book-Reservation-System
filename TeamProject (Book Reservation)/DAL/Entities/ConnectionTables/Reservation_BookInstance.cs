using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities.ConnectionTables
{
    public class Reservation_BookInstance : IEntity
    {
        public int ReservationID { get; set; }

        [ForeignKey(nameof(ReservationID))]
        public Reservation Reservation;

        public int BookInstanceID { get; set; }

        [ForeignKey(nameof(BookInstanceID))]
        public BookInstance BookInstance;
    }
}
