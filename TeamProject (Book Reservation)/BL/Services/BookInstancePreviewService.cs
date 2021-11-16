using System;
using AutoMapper;
using BL.DTOs.Filters;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query.Operators;
using System.Collections.Generic;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.User;
using DAL.Enums;

namespace BL.Services
{
    public class BookInstancePreviewService : CRUDService<BookInstanceDTO, BookInstance>, IBookInstanceService
    {                                                                                                
        private readonly QueryObject<BookInstancePrevDTO, BookInstance> _queryObject;

        public BookInstancePreviewService(IRepository<BookInstance> repo, 
                                          IMapper mapper, 
                                          QueryObject<BookInstancePrevDTO, BookInstance> queryObject) : base(repo, mapper)
        {
            _queryObject = queryObject;
        }

        public IEnumerable<BookInstancePrevDTO> GetBookInstancesByUser(UserDTO user, int pageNumber = 1, int pageSize = 20)
        {
            FilterDto filter = new FilterDto()
            {
                Predicate = new PredicateDto(nameof(BookInstance.BookOwnerId), user.Id, ValueComparingOperator.Equal),
                RequestedPageNumber = pageNumber,
                PageSize = pageSize
            };

            return _queryObject.ExecuteQuery(filter).Items;
        }

        public IEnumerable<BookInstanceDTO> GetBookInstancesByDate(BookDTO book, DateTime from, DateTime to, int pageNumber = 1, int pageSize = 20)
        {
            throw new NotImplementedException();
        }
    }
}
