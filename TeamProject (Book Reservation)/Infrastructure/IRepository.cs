using DAL.Entities;

namespace Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        void Delete(TEntity entityToDelete);
        void Delete(int id);
        TEntity GetByID(int id);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}