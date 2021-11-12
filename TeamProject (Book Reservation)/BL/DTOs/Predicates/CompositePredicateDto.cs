using Infrastructure.Query.Operators;
using System.Collections.Generic;
using System.Linq;

namespace BL.DTOs.Filters
{
    public class CompositePredicateDto : IPredicateDto
    {
        public List<IPredicateDto> Predicates { get; private set; }
        public LogicalOperator Operator { get; private set; }

        public CompositePredicateDto(IEnumerable<IPredicateDto> predicates, LogicalOperator oper)
        {
            Predicates = predicates.ToList();
            Operator = oper;
        }
    }
}