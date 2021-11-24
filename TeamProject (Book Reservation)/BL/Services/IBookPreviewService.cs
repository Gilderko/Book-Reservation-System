using BL.DTOs.Entities.Book;
using BL.DTOs.Filters;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IBookPreviewService : ICRUDService<BookPrevDTO, Book>
    {
        public Task<IEnumerable<BookPrevDTO>> GetBookPreviewsByFilter(FilterDto filter);
    }
}
