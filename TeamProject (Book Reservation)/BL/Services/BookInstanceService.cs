using AutoMapper;
using BL.DTOs.Filters;
using BL.DTOs.FullVersions;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class BookInstanceService
    {
        private readonly IRepository<BookInstance> _repository;
        private readonly IMapper _mapper;
        private readonly QueryObject<BookInstanceDTO, BookInstance> _queryObject;

        public BookInstanceService(IRepository<BookInstance> repo, IMapper mapper, QueryObject<BookInstanceDTO, BookInstance> queryObject)
        {
            _repository = repo;
            _mapper = mapper;
            _queryObject = queryObject;
        }

        public IEnumerable<BookInstanceDTO> GetBookInstancesByDate(BookDTO book, DateTime from, DateTime to, int pageNumber = 1, int pageSize = 20)
        {
            List<IPredicateDto> predicates = new()
            {
                new PredicateDto("BookTemplateID", book.Id, ValueComparingOperator.Equal),
                new PredicateDto("DateFrom", from.ToString("YYYY-MM-DD"), ValueComparingOperator.GreaterThanOrEqual),
                new PredicateDto("DateTill", to.ToString("YYYY-MM-DD"), ValueComparingOperator.LessThanOrEqual)
            };

            FilterDto filter = new()
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
