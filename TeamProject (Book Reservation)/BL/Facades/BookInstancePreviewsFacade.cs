using System;
using System.Collections;
using BL.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.User;
using DAL.Entities;

namespace BL.Facades
{
    public class BookInstancePreviewsFacade
    {
        private IBookInstancePreviewService _service;

        public BookInstancePreviewsFacade(IBookInstancePreviewService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<BookInstancePrevDTO>> GetBookInstancePrevsByUser(UserDTO user, int pageNumber, int pageSize)
        {
            return await _service.GetBookInstancePrevsByUser(user, pageNumber, pageSize);
        }

        public async Task<IEnumerable<BookInstancePrevDTO>> GetAvailableInstancePrevsByDate(BookDTO book, DateTime from,
            DateTime to)
        {
            return await _service.GetAvailableInstancePrevsByDate(book, from, to);
        }
    }
}
