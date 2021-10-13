using DAL.Query.Predicates;

namespace DAL.Query
{
    public interface IQuery<TEntity> where TEntity : class
    {
        public void Where(IPredicate rootPredicate);
        public void SortBy(string sortAccordingTo, bool ascendingOrder);
        public void Page(int pageToFetch, int pageSize);
        public abstract QueryResult<TEntity> ExecuteAsync();
    }
}
