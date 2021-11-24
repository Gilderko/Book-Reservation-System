using DAL.Entities;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        void Delete(TEntity entityToDelete);
        void DeleteById(int id);
        Task<TEntity> GetByID(int id, string[] refsToLoad = null, string[] collectionsToLoad = null);
        Task Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}