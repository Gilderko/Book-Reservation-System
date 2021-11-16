using System.Collections.Generic;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.EBook;
using BL.DTOs.Filters;
using BL.Services;
using DAL.Entities;

namespace BL.Facades
{
    public class EBookPreviewFacade
    {
        private EBookPreviewService<EBookPrevDTO, EBook> _service;
        

        public EBookPreviewFacade(EBookPreviewService<EBookPrevDTO, EBook> service)
        {
            _service = service;
        }

        public IEnumerable<BookPrevDTO> GetEBookPrevsByFilter(FilterDto filter)
        {
            return _service.GetEBookPrevsByFilter(filter);
        }
    }
}