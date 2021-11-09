namespace BL.DTOs.Filters
{
    public class FilterDto
    {
        public IPredicateDto Predicate { get; set; }
        public int? RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortCriteria { get; set; }
        public bool SortAscending { get; set; }

        public string[] _refsToLoad { get; private set; }

        public string[] _collectionsToLoad { get; private set; } 

        public void LoadExplicitReferences(params string[] referencesToLoad)
        {
            _refsToLoad = referencesToLoad;
        }

        public void LoadExplicitCollections(params string[] collectionsToLoad)
        {
            _collectionsToLoad = collectionsToLoad;
        }
    }
}