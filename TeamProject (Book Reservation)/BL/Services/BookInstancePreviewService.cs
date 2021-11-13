using AutoMapper;
using BL.DTOs.Filters;
using BL.DTOs.FullVersions;
using BL.DTOs.Previews;
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
    public class BookInstancePreviewService
    {
        private readonly IRepository<BookInstance> _repository;
        private readonly IMapper _mapper;
        private readonly QueryObject<BookInstancePrevDTO, BookInstance> _queryObject;

        public BookInstancePreviewService(IRepository<BookInstance> repo, IMapper mapper, QueryObject<BookInstancePrevDTO, BookInstance> queryObject)
        {
            _repository = repo;
            _mapper = mapper;
            _queryObject = queryObject;
        }

        public IEnumerable<BookInstancePrevDTO> GetBookInstancesByUser(UserDTO user, int pageNumber = 1, int pageSize = 20)
        {
            FilterDto filter = new()
            {
                Predicate = new PredicateDto("BookOwnerId", user.Id, ValueComparingOperator.Equal),
                RequestedPageNumber = pageNumber,
                PageSize = pageSize
            };

            return _queryObject.ExecuteQuery(filter).Items;
        }
    }
}
