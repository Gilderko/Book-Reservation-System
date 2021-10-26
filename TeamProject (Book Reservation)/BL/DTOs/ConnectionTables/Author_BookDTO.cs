using BL.DTOs;
using BL.DTOs.FullVersions;

namespace DAL.Entities.ConnectionTables
{
    public class Author_BookDTO : IEntityDTO
    {
        public int AuthorID { get; set; }

        public AuthorDTO Author;

        public int BookID { get; set; }

        public BookDTO Book;
    }
}
