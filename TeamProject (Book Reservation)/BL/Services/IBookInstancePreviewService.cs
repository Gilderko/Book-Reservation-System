using System;
using DAL.Entities;
using System.Collections.Generic;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.User;

namespace BL.Services
{
    public interface IBookInstancePreviewService : ICRUDService<BookInstancePrevDTO, BookInstance>
    {

        public IEnumerable<BookInstancePrevDTO> GetBookInstancePrevsByUser(UserDTO user, int pageNumber = 1,
            int pageSize = 20);

        public IEnumerable<BookInstancePrevDTO> GetAvailableInstancePrevsByDate(BookDTO book, DateTime from, DateTime to,
            int pageNumber = 1, int pageSize = 20);
    }
}
