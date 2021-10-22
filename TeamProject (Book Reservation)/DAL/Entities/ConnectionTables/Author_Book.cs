using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities.ConnectionTables
{
    public class Author_Book : IEntity
    {
        public int AuthorID { get; set; }

        [ForeignKey(nameof(AuthorID))]
        public Author Author;

        public int BookID { get; set; }

        [ForeignKey(nameof(BookID))]
        public Book Book;
    }
}
