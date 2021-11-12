using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities.ConnectionTables
{
    public class BookCollection_Book : IEntity
    {
        public int BookCollectionID { get; set; }

        [ForeignKey(nameof(BookCollectionID))]
        public BookCollection BookCollect { get; set; }

        public int BookID { get; set; }

        [ForeignKey(nameof(BookID))]
        public Book Book { get; set; }
    }
}
