using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.ConnectionTables
{
    public class Book_Genre
    {
        public int GenreID { get; set; }

        [ForeignKey(nameof(GenreID))]
        public Genre BookGenre;

        public int BookID { get; set; }

        [ForeignKey(nameof(BookID))]
        public Book Book;
    }
}
