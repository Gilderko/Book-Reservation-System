using System;
using DAL.Entities;
using System.Collections.Generic;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.User;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IBookInstancePreviewService : ICRUDService<BookInstancePrevDTO, BookInstance>
    {

        public Task<IEnumerable<BookInstancePrevDTO>> GetBookInstancePrevsByUser(UserDTO user, int pageNumber = 1,
            int pageSize = 20);

        public Task<IEnumerable<BookInstancePrevDTO>> GetAvailableInstancePrevsByDate(BookDTO book, DateTime from, DateTime to,
            int pageNumber = 1, int pageSize = 20);
    }
}
