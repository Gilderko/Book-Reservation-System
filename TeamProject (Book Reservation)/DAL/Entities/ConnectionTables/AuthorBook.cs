using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities.ConnectionTables
{
    public class AuthorBook : IEntity
    {
        public int AuthorID { get; set; }

        [ForeignKey(nameof(AuthorID))]
        public Author Author { get; set; }

        public int BookID { get; set; }

        [ForeignKey(nameof(BookID))]
        public Book Book { get; set; }
    }
}
