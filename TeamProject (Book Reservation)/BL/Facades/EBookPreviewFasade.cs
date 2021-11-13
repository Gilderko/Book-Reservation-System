using System.Collections.Generic;
using BL.DTOs.Filters;
using BL.DTOs.Previews;
using BL.Services;

namespace BL.Facades
{
    public class EBookPreviewFacade
    {
        private EBookPreviewService _service;
        

        public EBookPreviewFacade(EBookPreviewService service)
        {
            _service = service;
        }

        public IEnumerable<BookPrevDTO> GetEBookPrevsByFilter(FilterDto filter)
        {
            return _service.GetEBookPrevsByFilter(filter);
        }
    }
}