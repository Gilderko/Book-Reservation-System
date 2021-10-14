using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using DAL.Query.Operators;
using DAL.Query.Predicates;
using Microsoft.EntityFrameworkCore;

namespace DAL.Query
{
    public class QueryBase<TEntity> : IQuery<TEntity> where TEntity : class
    {
        public string SortAccordingTo { get; private set; }
        public bool UseAscendingOrder { get; private set; }
        public IPredicate Predicate { get; private set; }
        public int PageSize { get; private set; }
        public int DesiredPage { get; private set; }

        private BookRentalDbContext DatabaseContext;

        private string _querySql;

        private string _where = "";
        private string _sortBy = "";
        private string _page = "";
        private int _pageNumber;
        private int _pageSize;
        private bool _pagingEnabled = false;
        private string[] _refsToLoad = new string[0];
        private string[] _collectionsToLoad = new string[0];

        private readonly Dictionary<ValueComparingOperator, string> _valueOperators = new Dictionary<ValueComparingOperator, string>
        {
            { ValueComparingOperator.None, "IS NULL"},
            { ValueComparingOperator.GreaterThan, ">" },
            { ValueComparingOperator.GreaterThanOrEqual, ">=" },
            { ValueComparingOperator.Equal, "=" },
            { ValueComparingOperator.NotEqual, "<>" },
            { ValueComparingOperator.LessThan, "<" },
            { ValueComparingOperator.LessThanOrEqual, "<=" }
        };

        private string PredicateToString(IPredicate predicate)
        {
            if (predicate is SimplePredicate)
            {
                var simplePred = (SimplePredicate)predicate;               
                string cmpValFormat = simplePred.ComparedValue is string ? $"'{simplePred.ComparedValue}'" : $"{simplePred.ComparedValue}";

                return $"{simplePred.TargetPropertyName} {_valueOperators[simplePred.ValueComparingOperator]} {cmpValFormat}";
            }

            var compositePredicate = (CompositePredicate)predicate;
            var predicates = compositePredicate.Predicates.ToList();

            string result = PredicateToString(predicates[0]);

            foreach (var pred in predicates.Skip(1))
            {
                string logOper = compositePredicate.Operator == LogicalOperator.AND ? "AND" : "OR";
                result += $"{logOper} {PredicateToString(pred)}";
            }

            return result;
        }

        public void Where(IPredicate rootPredicate)
        {
            _where = $"WHERE {PredicateToString(rootPredicate)}";
        }

        public void SortBy(string sortAccordingTo, bool ascendingOrder)
        {
            string order = ascendingOrder ? "ASC" : "DESC"; 

            _sortBy = $"ORDER BY {sortAccordingTo} {order}";
            _page = "OFFSET 0 ROWS";
        }

        public void Page(int ipageToFetch, int pageSize)
        {
            _pagingEnabled = true;
            _pageSize = pageSize;
            _pageNumber = ipageToFetch;

            SortBy("Id", true);
            _page = $"OFFSET {(ipageToFetch - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";
        }

        public QueryResult<TEntity> Execute()
        {
            _querySql += $"{_where} {_sortBy} {_page}";

            Console.WriteLine(_querySql);

            var entities = DatabaseContext.Set<TEntity>().FromSqlRaw(_querySql).ToList();

            foreach(var entry in entities)
            {
                foreach(string refToLoad in _refsToLoad)
                {
                    DatabaseContext.Entry<TEntity>(entry).Reference(refToLoad).Load();
                }

                foreach(string collectToLoad in _collectionsToLoad)
                {
                    DatabaseContext.Entry<TEntity>(entry).Collection(collectToLoad).Load();
                }
            }

            QueryResult<TEntity> result = new QueryResult<TEntity>
            {
                TotalItemsCount = entities.Count(),
                Items = entities.ToList()
            };

            if (_pagingEnabled)
            {
                result.RequestedPageNumber = _pageNumber;
                result.PageSize = _pageSize;
            }

            return result;
        }
        public void LoadExplicitReferences(params string[] referencesToLoad)
        {
            _refsToLoad = referencesToLoad;
        }

        public void LoadExplicitCollections(params string[] collectionsToLoad)
        {
            _collectionsToLoad = collectionsToLoad;
        }

        public QueryBase(BookRentalDbContext dbContext)
        {
            DatabaseContext = dbContext;
            _querySql = $"SELECT * FROM dbo.{DatabaseContext.Model.FindEntityType(typeof(TEntity)).GetTableName()} ";
        }
    }
}
