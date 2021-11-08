using System.Collections.Generic;

namespace BL.DTOs
{
    public class QueryResultDTO<TEntityDTO> where TEntityDTO : class, IEntityDTO
    {
        public long TotalItemsCount { get; set; }
        public int? RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<TEntityDTO> Items { get; set; }
        public bool PagingEnabled { get; set; }

        public QueryResultDTO()
        {

        }            
    }
}