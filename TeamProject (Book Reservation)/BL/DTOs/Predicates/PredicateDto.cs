using Infrastructure.Query.Operators;

namespace BL.DTOs.Filters
{
    public class PredicateDto : IPredicateDto
    {
        public string TargetPropertyName { get; private set; }
        public object ComparedValue { get; private set; }
        public ValueComparingOperator ValueComparingOperator { get; private set; }

        public PredicateDto(string property, object value, ValueComparingOperator oper)
        {
            TargetPropertyName = property;
            ComparedValue = value;
            ValueComparingOperator = oper;
        }
    }
}