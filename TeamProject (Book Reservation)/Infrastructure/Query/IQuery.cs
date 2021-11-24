using DAL.Entities;
using Infrastructure.Query.Predicates;
using System.Threading.Tasks;

namespace Infrastructure.Query
{
    public interface IQuery<TEntity> where TEntity : class, IEntity
    {
        public void Where(IPredicate rootPredicate);
        public void SortBy(string sortAccordingTo, bool ascendingOrder);
        public void Page(int pageToFetch, int pageSize);
        public void LoadExplicitReferences(params string[] referencesToLoad);
        public void LoadExplicitCollections(params string[] collectionsToLoad);
        public abstract Task<QueryResult<TEntity>> Execute();
    }
}
