using DAL;
using DAL.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

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

        public TEntity GetByID(int id, string[] refsToLoad = null, string[] collectionsToLoad = null)
        {
            Func<EBook, string[]> square = x => new string[] { nameof(x.Authors), nameof(x.Genres) };


            string[] localRefsToLoad = refsToLoad;
            string[] localCollectionsToLoad = collectionsToLoad;

            if (localRefsToLoad == null)
            {
                localRefsToLoad = Array.Empty<string>();
            }
            if (localCollectionsToLoad == null)
            {
                localCollectionsToLoad = Array.Empty<string>();
            }

            TEntity loadedEntity = dbSet.Find(id);

            foreach (string refToLoad in localRefsToLoad)
            {
                dbContext.Entry<TEntity>(loadedEntity).Reference(refToLoad).Load();
            }

            foreach (string collectToLoad in localCollectionsToLoad)
            {
                dbContext.Entry<TEntity>(loadedEntity).Collection(collectToLoad).Load();
            }

            return dbSet.Find(id);
        }

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
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