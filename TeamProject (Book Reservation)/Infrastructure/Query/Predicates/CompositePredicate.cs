using System.Collections.Generic;
using System.Linq;
using Infrastructure.Query.Operators;

namespace Infrastructure.Query.Predicates
{
    public class CompositePredicate : IPredicate
    {
        public List<IPredicate> Predicates { get; private set; }
        public LogicalOperator Operator { get; private set; }

        public CompositePredicate(IEnumerable<IPredicate> predicates, LogicalOperator oper)
        {
            Predicates = predicates.ToList();
            Operator = oper;
        }
    }
}
