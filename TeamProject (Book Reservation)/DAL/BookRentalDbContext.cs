using DAL.Entities;
using DAL.Entities.ConnectionTables;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class BookRentalDbContext : DbContext
    {
        // Entity Tables
        public DbSet<User> Users { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<BookInstance> BookInstances { get; set; }

        public DbSet<BookCollection> BookCollections { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<EBook> EBookTemplates { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<EReader> EReaderTemplates { get; set; }

        public DbSet<EReaderInstance> EReaderInstances { get; set; }

        // Connection Tables

        public DbSet<AuthorBook> Author_Books { get; set; }

        public DbSet<BookGenre> Book_Genres { get; set; }

        public DbSet<BookCollectionBook> BookCollection_Books { get; set; }

        public DbSet<ReservationBookInstance> Reservation_BookInstances { get; set; }

        private string ConnectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=BookReservation";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);

            optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Author Book
            modelBuilder.Entity<AuthorBook>()
                .HasKey(obj => new { obj.BookID, obj.AuthorID });

            // Book Genre
            modelBuilder.Entity<BookGenre>()
                .HasKey(obj => new { obj.GenreID, obj.BookID });


            // BookCollection Book
            modelBuilder.Entity<BookCollectionBook>()
                .HasKey(obj => new { obj.BookID, obj.BookCollectionID });


            // Reservation BookInstance
            modelBuilder.Entity<ReservationBookInstance>()
                .HasKey(obj => new { obj.ReservationID, obj.BookInstanceID });

            // EReader EBook
            modelBuilder.Entity<EBookEReaderInstance>()
                .HasKey(obj => new { obj.EBookID, obj.EReaderInstanceID });

            modelBuilder.Entity<Reservation>()
                .HasOne(obj => obj.User)
                .WithMany(obj => obj.Reservations)
                .HasForeignKey(obj => obj.UserID)                
                .OnDelete(DeleteBehavior.Restrict)                
                .IsRequired(false);

            modelBuilder.Entity<Reservation>()
                .HasOne(obj => obj.EReader)
                .WithMany(obj => obj.Reservations)
                .HasForeignKey(obj => obj.EReaderID)                
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
