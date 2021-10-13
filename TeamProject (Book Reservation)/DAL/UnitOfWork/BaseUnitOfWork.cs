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
    public class BaseUnitOfWork : IUnitOfWork
    {
        public DbContext Context  { get; private set; }

        public BaseUnitOfWork(BookRentalDbContext dbContextInit)
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
