using DAL.Entities;

namespace Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        void Delete(TEntity entityToDelete);
        void DeleteById(int id);
        TEntity GetByID(int id, string[] refsToLoad = null, string[] collectionsToLoad = null);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}