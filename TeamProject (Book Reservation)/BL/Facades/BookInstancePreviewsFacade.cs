using System;
using System.Collections;
using BL.Services;
using System.Collections.Generic;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.User;
using DAL.Entities;

namespace BL.Facades
{
    public class BookInstancePreviewsFacade
    {
        private BookInstancePreviewService _service;

        public BookInstancePreviewsFacade(BookInstancePreviewService service)
        {
            _service = service;
        }

        public IEnumerable<BookInstancePrevDTO> GetBookInstancePrevsByUser(UserDTO user, int pageNumber, int pageSize)
        {
            return _service.GetBookInstancePrevsByUser(user, pageNumber, pageSize);
        }

        public IEnumerable<BookInstancePrevDTO> GetAvailableInstancePrevsByDate(BookDTO book, DateTime from,
            DateTime to)
        {
            return _service.GetAvailableInstancePrevsByDate(book, from, to);
        }
    }
}
