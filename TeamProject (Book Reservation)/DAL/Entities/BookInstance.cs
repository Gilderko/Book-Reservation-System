using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class BookInstance : BaseEntity
    {
        public int BookTemplateID { get; set; }

        [ForeignKey(nameof(BookTemplateID))]
        public BookTemplate FromBookTemplate { get; set; }

        public ICollection<Reservation> AllReservations { get; set; }
    }
}
