using DAL.Enums;
using DAL.Entities.ConnectionTables;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{

    public class Genre
    {
        [Key]
        public GenreType GenreID { get; set; }

        // Many to many relationships

        public ICollection<Book_Genre> Books { get; set; }
    }
}
