using BL.DTOs.FullVersions;

namespace BL.DTOs.ConnectionTables
{
    public class Book_GenreDTO : IEntityDTO
    {
        public int BookID { get; set; }

        public BookDTO Book { get; set; }

        public int GenreID { get; set; }

        public GenreDTO Genre { get; set; }
    }
}
