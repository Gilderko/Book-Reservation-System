using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.ConnectionTables;

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

        // Many to many relationships

        public ICollection<BookCollection_Book> Books { get; set; }
    }
}
