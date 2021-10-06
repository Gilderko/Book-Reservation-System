using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.ConnectionTables;

namespace DAL.Entities
{
    public class BookInstance : BaseEntity
    {
        public int BookTemplateID { get; set; }

        [ForeignKey(nameof(BookTemplateID))]
        public Book FromBookTemplate { get; set; }

        public ICollection<Reservation_BookInstance> AllReservations { get; set; }
    }
}
