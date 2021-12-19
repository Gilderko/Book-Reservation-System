using BL.DTOs.Entities.Book;
using BL.DTOs.Filters;
using BL.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class EBookPreviewFacade
    {
        private IEBookPreviewService _service;


        public EBookPreviewFacade(IEBookPreviewService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<BookPrevDTO>> GetEBookPrevsByFilter(FilterDto filter)
        {
            return await _service.GetEBookPrevsByFilter(filter);
        }
    }
}