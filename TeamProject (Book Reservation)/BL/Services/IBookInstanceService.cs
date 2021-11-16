using DAL.Entities;
using System;
using System.Collections.Generic;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookInstance;

namespace BL.Services
{
    public interface IBookInstanceService<TEntityDTO, TEntity> : ICRUDService<TEntityDTO, TEntity> where TEntity : BookInstance
                                                                                             where TEntityDTO : BookInstanceDTO
    {
        public IEnumerable<BookInstanceDTO> GetBookInstancesByDate(BookDTO book, DateTime from, DateTime to,
            int pageNumber = 1, int pageSize = 20);
    }
}
