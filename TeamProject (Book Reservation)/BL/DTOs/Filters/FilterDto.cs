namespace BL.DTOs.Filters
{
    public class FilterDto
    {
        public IPredicateDto Predicate { get; set; }
        public int? RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortCriteria { get; set; }
        public bool SortAscending { get; set; }
    }
}