using DAL.Entities;
using DAL.Enums;
using Microsoft.EntityFrameworkCore;
using DAL.Entities.ConnectionTables;

namespace DAL
{
    public class BookRentalDbContext : DbContext
    {
        // Entity Tables
        public DbSet<User> Users { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> BookTemplates { get; set; }

        public DbSet<BookInstance> BookInstances { get; set; }

        public DbSet<BookCollection> BookCollections { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<EBook> EBookTemplates { get; set; }

        public DbSet<EBookInstance> EBookInstances { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<EReader> EReaderTemplates { get; set; }

        public DbSet<EReaderInstance> EReaderInstances { get; set; }

        // Connection Tables

        public DbSet<Author_Book> Author_Books { get; set;}

        public DbSet<Book_Genre> Book_Genres { get; set; }

        public DbSet<BookCollection_Book> BookCollection_Books { get; set; }

        public DbSet<Reservation_BookInstance> Reservation_BookInstances { get; set; }

        private string ConnectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=BookReservation";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Genre>()
                .Property(genre => genre.Id)
                .HasConversion(
                // Conversion to provider
                genreID => (int)genreID,
                // Conversion from provider
                genreID => (GenreType)genreID);

            // Author Book
            modelBuilder.Entity<Author_Book>()
                .HasKey(obj => new { obj.BookID, obj.AuthorID });

            // Book Genre
            modelBuilder.Entity<Book_Genre>()
                .HasKey(obj => new { obj.GenreID, obj.BookID });

            // BookCollection Book
            modelBuilder.Entity<BookCollection_Book>()
                .HasKey(obj => new { obj.BookID, obj.BookCollectionID });
 

            // Reservation BookInstance
            modelBuilder.Entity<Reservation_BookInstance>()
                .HasKey(obj => new { obj.ReservationID, obj.BookInstanceID });

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
