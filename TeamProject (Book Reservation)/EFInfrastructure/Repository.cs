using DAL;
using DAL.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EFInfrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly BookRentalDbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public Repository(IUnitOfWork unitOfWork)
        {
            dbContext = ((UnitOfWork)unitOfWork).Context;
            dbSet = dbContext.Set<TEntity>();
        }

        public TEntity GetByID(int id)
        {
            return dbSet.Find(id);
        }

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(int id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public void Delete(TEntity entityToDelete)
        {
            if (dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}