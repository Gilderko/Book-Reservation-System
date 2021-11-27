using DAL;
using DAL.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<TEntity> GetByID(int id, string[] refsToLoad = null, string[] collectionsToLoad = null)
        {
            TEntity loadedEntity = await dbSet.FindAsync(id);

            if (loadedEntity != null)
            {
                if (refsToLoad != null)
                {
                    foreach (string refToLoad in refsToLoad)
                    {
                        await dbContext.Entry<TEntity>(loadedEntity).Reference(refToLoad).LoadAsync();
                    }
                }

                if (collectionsToLoad != null)
                {
                    foreach (string collectToLoad in collectionsToLoad)
                    {
                        await dbContext.Entry<TEntity>(loadedEntity).Collection(collectToLoad).LoadAsync();
                    }
                }
            }

            return loadedEntity;
        }

        public async Task Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void DeleteById(int id)
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