using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookInstance;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IBookInstancePreviewService : ICRUDService<BookInstancePrevDTO, BookInstance>
    {

        public Task<IEnumerable<BookInstancePrevDTO>> GetBookInstancePrevsByUser(int userId);

        public Task<IEnumerable<BookInstancePrevDTO>> GetAvailableInstancePrevsByDate(BookDTO book, DateTime from, DateTime to,
            int pageNumber = 1, int pageSize = 20);
    }
}
