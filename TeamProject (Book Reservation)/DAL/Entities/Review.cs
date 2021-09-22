using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Review : BaseEntity
    {
        [Column(TypeName = "Date")]
        public DateTime CreationDate { get; set; }

        [MaxLength(255)]
        public string Content { get; set; }

        [Range(0,5)]
        public int StarsAmmount { get; set; }

        public int BookTemplateID { get; set; }

        [ForeignKey(nameof(BookTemplateID))]
        public BookTemplate ReserveredBook { get; set; }

        public int UserID { get; set; }

        [ForeignKey(nameof(UserID))]
        public User User { get; set; }
    }
}
