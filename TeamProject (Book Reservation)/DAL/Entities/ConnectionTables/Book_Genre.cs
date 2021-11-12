using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities.ConnectionTables
{
    public class Book_Genre : IEntity
    {
        public int BookID { get; set; }

        [ForeignKey(nameof(BookID))]
        public Book Book { get; set; }

        public int GenreID { get; set; }

        [ForeignKey(nameof(GenreID))]
        public Genre Genre { get; set; }
    }
}
