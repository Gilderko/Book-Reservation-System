using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.ConnectionTables
{
    public class Author_Book
    {
        public int AuthorID { get; set; }

        [ForeignKey(nameof(AuthorID))]
        public Author Author; 
        
        public int BookID { get; set; }

        [ForeignKey(nameof(BookID))]
        public Book Book;
    }
}
