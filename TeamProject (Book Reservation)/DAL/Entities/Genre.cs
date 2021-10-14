using DAL.Enums;
using DAL.Entities.ConnectionTables;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{

    public class Genre
    {
        // Dont change the name of this property all properties need to have primary key "Id"
        [Key]
        public int Id { get; set; }

        // Many to many relationships

        public ICollection<Book_Genre> Books { get; set; }
    }
}
