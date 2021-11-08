using Infrastructure.Query.Operators;

namespace BL.DTOs.Filters
{
    public class Filter : IFilter
    {
        public string TargetPropertyName { get; private set; }
        public object ComparedValue { get; private set; }
        public ValueComparingOperator ValueComparingOperator { get; private set; }

        public Filter(string property, object value, ValueComparingOperator oper)
        {
            TargetPropertyName = property;
            ComparedValue = value;
            ValueComparingOperator = oper;
        }
        
        public int? RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortCriteria { get; set; }
        public bool SortAscending { get; set; }
    }
}