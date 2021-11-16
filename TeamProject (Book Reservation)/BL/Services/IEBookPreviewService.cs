using System.Collections.Generic;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.EBook;
using BL.DTOs.Filters;
using DAL.Entities;

namespace BL.Services
{
    public interface IEBookPreviewService : ICRUDService<EBookPrevDTO, EBook>
    {
        public IEnumerable<BookPrevDTO> GetEBookPrevsByFilter(FilterDto filter);
    }
}