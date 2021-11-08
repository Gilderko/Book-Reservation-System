using System.Collections.Generic;
using System.Linq;
using Infrastructure.Query.Operators;
using Infrastructure.Query.Predicates;

namespace BL.DTOs.Filters
{
    public class CompositeFilter : IFilter
    {
        public List<IFilter> Predicates { get; private set; }
        public LogicalOperator Operator { get; private set; }

        public CompositeFilter(IEnumerable<IFilter> predicates, LogicalOperator oper)
        {
            Predicates = predicates.ToList();
            Operator = oper;
        }
    }
}