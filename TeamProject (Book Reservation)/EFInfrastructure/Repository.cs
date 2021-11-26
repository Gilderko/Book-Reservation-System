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

            List<Task> loadingTasks = new List<Task>();

            foreach (string refToLoad in localRefsToLoad)
            {
                loadingTasks.Add(dbContext.Entry<TEntity>(loadedEntity).Reference(refToLoad).LoadAsync());
            }

            foreach (string collectToLoad in localCollectionsToLoad)
            {
                loadingTasks.Add(dbContext.Entry<TEntity>(loadedEntity).Collection(collectToLoad).LoadAsync());
            }

            Task.WaitAll(loadingTasks.ToArray());

            var result = await dbSet.FindAsync(id);
            dbContext.Entry(result).State = EntityState.Detached;
            return result;
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
            if (dbContext.Entry(entityToUpdate).State == EntityState.Detached)
            {
                dbSet.Attach(entityToUpdate);
            }
            dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}