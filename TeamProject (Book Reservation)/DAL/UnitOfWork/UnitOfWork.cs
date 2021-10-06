using DAL.Entities;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext dbContext;

        public IRepository<Author> Authors { get; private set; }

        public IRepository<Book> Books { get; private set; }

        public IRepository<BookInstance> BookInstaces { get; private set; }

        public IRepository<BookCollection> BookCollections { get; private set; }

        public IRepository<EBook> EBooks { get; private set; }

        public IRepository<EBookInstance> EbookInstances { get; private set; }

        public IRepository<EReader> EReaders { get; private set; }

        public IRepository<EReaderInstance> EReaderInstances { get; private set; }

        public IRepository<Genre> Genres { get; private set; }

        public IRepository<Reservation> Reservations { get; private set; }

        public IRepository<Review> Reviews { get; private set; }

        public IRepository<User> Users { get; private set; }

        public UnitOfWork(DbContext dbContextInit)
        {
            dbContext = dbContextInit;

            Authors = new BaseRepository<Author>(dbContext);
            Books = new BaseRepository<Book>(dbContext);
            BookInstaces = new BaseRepository<BookInstance>(dbContext);
            BookCollections = new BaseRepository<BookCollection>(dbContext);
            EBooks = new BaseRepository<EBook>(dbContext);
            EbookInstances = new BaseRepository<EBookInstance>(dbContext);
            EReaders = new BaseRepository<EReader>(dbContext);
            EReaderInstances = new BaseRepository<EReaderInstance>(dbContext);
            Genres = new BaseRepository<Genre>(dbContext);
            Reservations = new BaseRepository<Reservation>(dbContext);
            Reviews = new BaseRepository<Review>(dbContext);
            Users = new BaseRepository<User>(dbContext);
        }        

        public void Commit()
        {
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
