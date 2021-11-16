using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookCollection;

namespace BL.DTOs.ConnectionTables
{
    public class BookCollectionBookDTO : IEntityDTO
    {
        public int BookCollectionID { get; set; }

        public BookCollectionDTO BookCollect { get; set; }

        public int BookID { get; set; }

        public BookDTO Book { get; set; }
    }
}
