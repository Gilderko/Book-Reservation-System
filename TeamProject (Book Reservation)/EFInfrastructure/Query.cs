using DAL;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query;
using Infrastructure.Query.Operators;
using Infrastructure.Query.Predicates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFInfrastructure
{
    public class Query<TEntity> : IQuery<TEntity> where TEntity : class, IEntity
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
            { ValueComparingOperator.LessThanOrEqual, "<=" },
            { ValueComparingOperator.Contains, "LIKE"},
            { ValueComparingOperator.In, "IN"},
            { ValueComparingOperator.NotIn, "NOT IN"}
        };

        private string PredicateToString(IPredicate predicate)
        {
            if (predicate is SimplePredicate)
            {
                var simplePred = (SimplePredicate)predicate;

                string cmpValFormat = string.Empty;
                if (simplePred.ComparedValue is string)
                {
                    if (simplePred.ValueComparingOperator == ValueComparingOperator.Contains)
                    {
                        cmpValFormat = $"'%{simplePred.ComparedValue}%'";
                    }
                    
                }
                else if (simplePred.ComparedValue is IEnumerable<int>)
                {
                    StringBuilder varCompareString = new StringBuilder();
                    varCompareString.Append('(');

                    foreach (var value in (simplePred.ComparedValue as IEnumerable<int>))
                    {
                        varCompareString.Append(value);
                        varCompareString.Append(',');
                    }

                    varCompareString.Remove(varCompareString.Length - 1, 1);
                    varCompareString.Append(')');

                    cmpValFormat = varCompareString.ToString();
                }
                else if (simplePred.ComparedValue is IEnumerable<string>)
                {
                    StringBuilder varCompareString = new StringBuilder();
                    varCompareString.Append('(');

                    foreach (var value in (simplePred.ComparedValue as IEnumerable<string>))
                    {
                        varCompareString.Append('\'');
                        varCompareString.Append(value);
                        varCompareString.Append('\'');
                        varCompareString.Append(',');

                    }

                    varCompareString.Remove(varCompareString.Length - 1, 1);
                    varCompareString.Append(')');

                    cmpValFormat = varCompareString.ToString();
                }
                else 
                {
                    cmpValFormat = $"{simplePred.ComparedValue}";
                }

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

            foreach (var entry in entities)
            {
                foreach (string refToLoad in _refsToLoad)
                {
                    DatabaseContext.Entry<TEntity>(entry).Reference(refToLoad).Load();
                }

                foreach (string collectToLoad in _collectionsToLoad)
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

        public Query(IUnitOfWork unitOfWork)
        {
            DatabaseContext = ((UnitOfWork)unitOfWork).Context;
            _querySql = $"SELECT * FROM dbo.{DatabaseContext.Model.FindEntityType(typeof(TEntity)).GetTableName()} ";
        }
    }
}
