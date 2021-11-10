using DAL.Entities.ConnectionTables;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class BookInstance : BaseEntity
    {

        public int BookOwnerId { get; set; }

        [ForeignKey(nameof(BookTemplateID))]
        public User Owner { get; set; }

        public int BookTemplateID { get; set; }

        [ForeignKey(nameof(BookTemplateID))]
        public Book FromBookTemplate { get; set; }

        public ICollection<Reservation_BookInstance> AllReservations { get; set; }
    }
}
