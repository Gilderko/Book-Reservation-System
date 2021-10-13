using DAL.Entities;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        public DbContext Context { get; }
    }
}
