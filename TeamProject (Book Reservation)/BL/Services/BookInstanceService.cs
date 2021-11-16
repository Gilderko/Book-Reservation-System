using AutoMapper;
using BL.DTOs.Filters;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query.Operators;
using System;
using System.Collections.Generic;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookInstance;

namespace BL.Services
{
    public class BookInstanceService : CRUDService<BookInstanceDTO, BookInstance>, IBookInstanceService
    {
        private readonly QueryObject<BookInstanceDTO, BookInstance> _queryObject;

        public BookInstanceService(IRepository<BookInstance> repo, 
                                   IMapper mapper, 
                                   QueryObject<BookInstanceDTO, BookInstance> resQueryObject) : base(repo, mapper)
        {
            _queryObject = resQueryObject;
        }

        public IEnumerable<BookInstanceDTO> GetBookInstancesByDate(BookDTO book, DateTime from, DateTime to, int pageNumber = 1, int pageSize = 20)
        {
            List<IPredicateDto> predicates = new List<IPredicateDto>()
            {
                new PredicateDto(nameof(BookInstanceDTO.BookTemplateID), book.Id, ValueComparingOperator.Equal),
                // new PredicateDto("DateFrom", from.ToString("YYYY-MM-DD"), ValueComparingOperator.GreaterThanOrEqual),
                // new PredicateDto("DateTill", to.ToString("YYYY-MM-DD"), ValueComparingOperator.LessThanOrEqual)
            };

            FilterDto filter = new FilterDto()
            {
                Predicate = new CompositePredicateDto(predicates, LogicalOperator.AND),
                RequestedPageNumber = pageNumber,
                PageSize = pageSize
            };

            // TODO

            return _queryObject.ExecuteQuery(filter).Items;
        }
    }
}
