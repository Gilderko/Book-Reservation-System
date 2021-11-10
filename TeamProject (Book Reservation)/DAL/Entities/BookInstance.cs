﻿using DAL.Entities.ConnectionTables;
using DAL.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class BookInstance : BaseEntity
    {
        public BookInstanceCondition Conditon { get; set; }

        public int BookOwnerId { get; set; }


        public int BookTemplateID { get; set; }

        [ForeignKey(nameof(BookTemplateID))]
        public Book FromBookTemplate { get; set; }

        public ICollection<Reservation_BookInstance> AllReservations { get; set; }
    }
}
