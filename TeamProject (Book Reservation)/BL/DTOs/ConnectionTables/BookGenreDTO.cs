using BL.DTOs.FullVersions;

namespace BL.DTOs.ConnectionTables
{
    public class BookGenreDTO : IEntityDTO
    {
        public int BookID { get; set; }

        public BookDTO Book { get; set; }

        public int GenreID { get; set; }

        public GenreDTO Genre { get; set; }
    }
}
