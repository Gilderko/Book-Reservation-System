using System.Collections.Generic;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.EBook;
using BL.DTOs.Filters;
using DAL.Entities;

namespace BL.Services
{
    public interface IEBookPreviewService<TEntityDTO, TEntity> : ICRUDService<TEntityDTO, TEntity> where TEntity : EBook
                                                                                             where TEntityDTO : EBookPrevDTO
    {
        public IEnumerable<BookPrevDTO> GetEBookPrevsByFilter(FilterDto filter);
    }
}