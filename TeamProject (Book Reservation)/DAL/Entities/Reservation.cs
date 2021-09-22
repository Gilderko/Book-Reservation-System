using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Reservation : BaseEntity
    {
        [Column(TypeName = "Date")]
        public DateTime DateFrom { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DateTill { get; set; }

        public int BookInstanceID { get; set; }

        [ForeignKey(nameof(BookInstanceID))]
        public BookInstance ReserveredBook { get; set; }

        public int UserID { get; set; }

        [ForeignKey(nameof(UserID))]
        public User User { get; set; }

        public int ERedeaderID { get; set; }

        [ForeignKey(nameof(ERedeaderID))]
        public EReader EReader { get; set; }
    }
}
