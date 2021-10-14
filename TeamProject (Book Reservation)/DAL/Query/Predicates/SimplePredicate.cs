using DAL.Query.Operators;

namespace DAL.Query.Predicates
{
    public class SimplePredicate : IPredicate
    {
        public string TargetPropertyName { get; private set; }
        public object ComparedValue { get; private set; }
        public ValueComparingOperator ValueComparingOperator { get; private set; }

        public SimplePredicate(string property, object value, ValueComparingOperator oper)
        {
            TargetPropertyName = property;
            ComparedValue = value;
            ValueComparingOperator = oper;
        }
    }
}
