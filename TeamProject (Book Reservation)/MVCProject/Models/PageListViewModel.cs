using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Models
{
    public class PagedListViewModel<T>
    {
        public PaginationViewModel Pagination { get; set; }
        public IEnumerable<T> List { get; set; } = new List<T>();

        public PagedListViewModel(PaginationViewModel pagination, IEnumerable<T> list)
        {
            Pagination = pagination;
            List = list;
        }
    }
}
