using BL.Services;
using System.Collections.Generic;
using BL.DTOs.Entities.BookCollection;
using BL.DTOs.Entities.User;
using DAL.Entities;

namespace BL.Facades
{
    public class BookCollectionPreviewsFacade
    {
        private IBookCollectionPreviewService _service;

        public BookCollectionPreviewsFacade(IBookCollectionPreviewService service)
        {
            _service = service;
        }

        public IEnumerable<BookCollectionPrevDTO> GetBookCollectionsByUser(UserDTO user, int pageNumber, int pageSize)
        {
            return _service.GetBookCollectionsByUser(user, pageNumber, pageSize);
        }
    }
}
