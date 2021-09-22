using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class BookCollection : BaseEntity
    {
        [MaxLength(128)]
        public string Title { get; set; }

        [MaxLength(256)]
        public string Description { get; set; }

        [Column(TypeName = "Date")]
        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User OwnerUser;

        public ICollection<BookTemplate> Books { get; set; }
    }
}
