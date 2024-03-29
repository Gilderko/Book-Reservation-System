﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities.ConnectionTables
{
    public class ReservationBookInstance : IEntity
    {
        // Set up in Model creator
        public int ReservationID { get; set; }

        [ForeignKey(nameof(ReservationID))]
        public Reservation Reservation { get; set; }

        public int BookInstanceID { get; set; }

        [ForeignKey(nameof(BookInstanceID))]
        public BookInstance BookInstance { get; set; }
    }
}
