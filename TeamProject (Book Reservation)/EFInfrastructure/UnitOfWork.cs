using DAL;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

namespace EFInfrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public BookRentalDbContext Context { get; private set; }

        public UnitOfWork(BookRentalDbContext dbContextInit)
        {
            Console.WriteLine("creating Unit of work");
            Context = dbContextInit;
        }

        public DbContext GetContext()
        {
            return Context;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
