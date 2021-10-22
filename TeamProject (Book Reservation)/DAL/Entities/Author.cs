using DAL.Entities.ConnectionTables;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Author : BaseEntity
    {
        [MaxLength(32)]
        public string Name { get; set; }

        [MaxLength(32)]
        public string Surname { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        // Many to many relationships

        public ICollection<Author_Book> AuthorsBooks { get; set; }
    }
}
