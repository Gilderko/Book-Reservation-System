using DAL;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query;
using Infrastructure.Query.Operators;
using Infrastructure.Query.Predicates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    else
                    {
                        cmpValFormat = $"'{simplePred.ComparedValue}'";
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

                    if (varCompareString[varCompareString.Length - 1] == '(')
                    {
                        varCompareString.Append("NULL");
                    }
                    else
                    {
                        varCompareString.Remove(varCompareString.Length - 1, 1);
                    }

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

                    if (varCompareString[varCompareString.Length - 1] == '(')
                    {
                        varCompareString.Append("NULL");
                    }
                    else
                    {
                        varCompareString.Remove(varCompareString.Length - 1, 1);
                    }

                    varCompareString.Append(')');

                    cmpValFormat = varCompareString.ToString();
                }
                else if (simplePred.ComparedValue is DateTime)
                {
                    var date = (DateTime) simplePred.ComparedValue;
                    cmpValFormat = String.Format("'{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}'", date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
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
                result += $" {logOper} {PredicateToString(pred)}";
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

            if (_sortBy == "")
            {
                SortBy(nameof(BaseEntity.Id), true);
            }
           
            _page = $"OFFSET {(ipageToFetch - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";
        }

        public async Task<QueryResult<TEntity>> Execute()
        {
            var countSql = _querySql + $"{_where}";
            _querySql += $"{_where} {_sortBy} {_page}";

            Console.WriteLine(_querySql);

            var entities = await DatabaseContext.Set<TEntity>().FromSqlRaw(_querySql).ToListAsync();
            var totalItemsCount = DatabaseContext.Set<TEntity>().FromSqlRaw(countSql).Count();

            foreach (var entry in entities)
            {
                if (_refsToLoad != null)
                {
                    foreach (string refToLoad in _refsToLoad)
                    {
                        await DatabaseContext.Entry<TEntity>(entry).Reference(refToLoad).LoadAsync();
                    }
                }

                if (_collectionsToLoad != null)
                {
                    foreach (string collectToLoad in _collectionsToLoad)
                    {
                        await DatabaseContext.Entry<TEntity>(entry).Collection(collectToLoad).LoadAsync();
                    }
                }
            }

            QueryResult<TEntity> result = new QueryResult<TEntity>
            {
                TotalItemsCount = totalItemsCount,
                Items = entities.ToList()
            };

            if (_pagingEnabled)
            {
                result.RequestedPageNumber = _pageNumber;
                result.PageSize = _pageSize;
            }

            ClearQuery();

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

        private void ClearQuery()
        {
            _querySql = $"SELECT * FROM dbo.{DatabaseContext.Model.FindEntityType(typeof(TEntity)).GetTableName()} ";
            _where = "";
            _sortBy = "";
            _page = "";
            _refsToLoad = new string[0];
            _collectionsToLoad = new string[0];
            _pageSize = 0;
            _pageNumber = 0;
            _pagingEnabled = false;
        }

        public Query(IUnitOfWork unitOfWork)
        {
            DatabaseContext = ((UnitOfWork)unitOfWork).Context;
            _querySql = $"SELECT * FROM dbo.{DatabaseContext.Model.FindEntityType(typeof(TEntity)).GetTableName()} ";
        }
    }
}
