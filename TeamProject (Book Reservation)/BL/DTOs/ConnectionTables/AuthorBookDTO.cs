using BL.DTOs.FullVersions;

namespace BL.DTOs.ConnectionTables
{
    public class AuthorBookDTO : IEntityDTO
    {
        public int AuthorID { get; set; }

        public AuthorDTO Author { get; set; }

        public int BookID { get; set; }

        public BookDTO Book { get; set; }
    }
}
