using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.ConnectionTables
{
    public class Reservation_BookInstance
    {
        public int ReservationID { get; set; }

        [ForeignKey(nameof(ReservationID))]
        public Reservation Reservation;

        public int BookInstanceID { get; set; }

        [ForeignKey(nameof(BookInstanceID))]
        public BookInstance BookInstance;
    }
}
