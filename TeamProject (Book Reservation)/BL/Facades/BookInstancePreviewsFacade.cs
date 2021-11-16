using BL.Services;
using System.Collections.Generic;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.User;
using DAL.Entities;

namespace BL.Facades
{
    public class BookInstancePreviewsFacade
    {
        private BookInstancePreviewService<BookInstanceDTO, BookInstance> _service;

        public BookInstancePreviewsFacade(BookInstancePreviewService<BookInstanceDTO, BookInstance> service)
        {
            _service = service;
        }

        public IEnumerable<BookInstancePrevDTO> GetBookInstancesByUser(UserDTO user, int pageNumber, int pageSize)
        {
            return _service.GetBookInstancesByUser(user, pageNumber, pageSize);
        }
    }
}
