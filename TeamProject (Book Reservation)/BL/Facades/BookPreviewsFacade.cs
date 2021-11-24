using System.Collections.Generic;
using System.Threading.Tasks;
using BL.Services;
using BL.DTOs.Entities.Book;
using BL.DTOs.Filters;
using DAL.Entities;

namespace BL.Facades
{
    public class BookPreviewsFacade
    {
        private IBookPreviewService _service;

        public BookPreviewsFacade(IBookPreviewService service)
        {
            _service = service;
        }
        
        public async Task<IEnumerable<BookPrevDTO>> GetBookPreviews(FilterDto filter)
        {
            return await _service.GetBookPreviewsByFilter(filter);
        }
    }
}
