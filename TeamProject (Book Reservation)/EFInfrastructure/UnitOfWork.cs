using DAL;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EFInfrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context  { get; private set; }

        public UnitOfWork(BookRentalDbContext dbContextInit)
        {
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
