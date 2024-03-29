﻿using DAL.Entities;
using System.Collections.Generic;

namespace Infrastructure.Query
{
    public class QueryResult<TEntity> where TEntity : class, IEntity
    {
        public int TotalItemsCount { get; set; }
        public int? RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<TEntity> Items { get; set; }
        public bool PagingEnabled { get; set; }

        public QueryResult()
        {

        }
    }
}
