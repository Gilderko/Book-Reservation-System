using System.Collections.Generic;

namespace DAL.Query
{
    public class QueryResult<TEntity> where TEntity : class
    {
        public long TotalItemsCount { get; private set; }
        public int RequestedPageNumber { get; private set; }
        public int PageSize { get; private set; }
        public IList<TEntity> Items { get; private set; }
        public bool PagingEnabled { get; private set; }

        public QueryResult()
        {

        }
    }
}
