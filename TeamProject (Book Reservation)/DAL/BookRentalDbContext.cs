using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BookRentalDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<BookTemplate> BookTemplates { get; set; }

        public DbSet<BookInstance> BookInstances { get; set; }

        public DbSet<BookCollection> BookCollections { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<EBookTemplate> EBookTemplates { get; set; }

        public DbSet<EBookInstance> EBookInstances { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<EReaderTemplate> EReaderTemplates { get; set; }

        public DbSet<EReaderInstance> EReaderInstances { get; set; }



        private string ConnectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=BookReservation";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);//.UseLazyLoadingProxies(); Ked lazy tak vsetko je virtual

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Genre>()
                .Property(genre => genre.GenreID)
                .HasConversion(
                // Conversion to provider
                genreID => (int) genreID,
                // Conversion from provider
                genreID => (GenreType) genreID);           

            base.OnModelCreating(modelBuilder);
        }
    }
}
