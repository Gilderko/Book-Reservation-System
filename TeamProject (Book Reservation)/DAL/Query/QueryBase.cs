using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using DAL.Query.Operators;
using DAL.Query.Predicates;

namespace DAL.Query
{
    public class QueryBase<TEntity> : IQuery<TEntity> where TEntity : class
    {
        public string SortAccordingTo { get; private set; }
        public bool UseAscendingOrder { get; private set; }
        public IPredicate Predicate { get; private set; }
        public int PageSize { get; private set; }
        public int DesiredPage { get; private set; }

        private BookRentalDbContext DbContext;

        private string _querySql = $"SELECT * FROM dbo.{nameof(TEntity)} ";

        private string _where = "";
        private string _sortBy = "";
        private string _page = "";

        private Dictionary<ValueComparingOperator, string> _valueOperators = new()
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

                return $"{simplePred.TargetPropertyName} {_valueOperators[simplePred.ValueComparingOperator]} {simplePred.ComparedValue}";
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
            _where = $"WHERE {}";
        }

        public void SortBy(string sortAccordingTo, bool ascendingOrder)
        {
            string order = ascendingOrder ? "ASC" : "DES"; 

            _sortBy = $"SORT BY {sortAccordingTo} {ascendingOrder}";
        }

        public void Page(int ipageToFetch, int pageSize)
        {
            _page = $"OFFSET {ipageToFetch * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";
        }

        public QueryResult<TEntity> ExecuteAsync()
        {
            return null;
        }

        public QueryBase(BookRentalDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
