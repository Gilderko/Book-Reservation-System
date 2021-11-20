using System.Collections.Generic;
using BL.Services;
using BL.DTOs.Entities.Book;
using BL.DTOs.Filters;
using DAL.Entities;

namespace BL.Facades
{
    public class BookPreviewsFacade
    {
        private BookPreviewService _service;

        public BookPreviewsFacade(BookPreviewService service)
        {
            _service = service;
        }
        
        public IEnumerable<BookPrevDTO> GetBookPreviews(FilterDto filter)
        {
            return _service.GetBookPreviewsByFilter(filter);
        }
    }
}
