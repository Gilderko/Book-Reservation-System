using DAL.Entities;
using DAL.Repository;
using System;
using System.Collections.Generic;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        // Repositories for entities
        IRepository<Author> Authors { get; }
        IRepository<Book> Books { get; }
        IRepository<BookInstance> BookInstaces { get; }
        IRepository<BookCollection> BookCollections { get; }
        IRepository<EBook> EBooks { get; }
        IRepository<EBookInstance> EbookInstances { get; }
        IRepository<EReader> EReaders { get; }
        IRepository<EReaderInstance> EReaderInstances { get; }
        IRepository<Genre> Genres { get; }
        IRepository<Reservation> Reservations { get; }
        IRepository<Review> Reviews { get; }
        IRepository<User> Users { get; }
        void Commit();
    }
}
