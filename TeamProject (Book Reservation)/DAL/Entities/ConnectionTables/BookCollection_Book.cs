using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.ConnectionTables
{
    public class BookCollection_Book
    {
        public int BookCollectionID { get; set; }

        [ForeignKey(nameof(BookCollectionID))]
        public BookCollection BookCollect;

        public int BookID { get; set; }

        [ForeignKey(nameof(BookID))]
        public Book Book;
    }
}
