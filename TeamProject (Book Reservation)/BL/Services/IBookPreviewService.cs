using BL.DTOs.Entities.Book;
using DAL.Entities;
using System.Collections.Generic;

namespace BL.Services
{
    public interface IBookPreviewService : ICRUDService<BookPrevDTO, Book>
    {
        public IEnumerable<BookPrevDTO> GetBookPreviewsByFilter(FilterDto filter);
    }
}
