using DAL.Entities.ConnectionTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Reservation : BaseEntity
    {
        [Column(TypeName = "Date")]
        public DateTime DateFrom { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DateTill { get; set; }

        public int? UserID { get; set; }

        [ForeignKey(nameof(UserID))]
        public User User { get; set; }

        public int? EReaderID { get; set; }

        [ForeignKey(nameof(EReaderID))]
        public EReaderInstance EReader { get; set; }

        // Many to many relationships

        public ICollection<ReservationBookInstance> BookInstances { get; set; }
    }
}
