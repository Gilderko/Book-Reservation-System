namespace DAL.Entities.ConnectionTables
{
    public class Book_GenreDTO : IEntityDTO
    {
        public int BookID { get; set; }

        public BookDTO Book;

        public int GenreID { get; set; }

        public GenreDTO Genre;
    }
}
