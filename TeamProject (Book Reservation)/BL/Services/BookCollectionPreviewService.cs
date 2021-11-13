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
    public class BookCollectionPreviewService
    {
        private readonly IRepository<BookCollection> _repository;
        private readonly IMapper _mapper;
        private readonly QueryObject<BookCollectionPrevDTO, BookCollection> _queryObject;

        public BookCollectionPreviewService(IRepository<BookCollection> repo, IMapper mapper, QueryObject<BookCollectionPrevDTO, BookCollection> queryObject)
        {
            _repository = repo;
            _mapper = mapper;
            _queryObject = queryObject;
        }

        public IEnumerable<BookCollectionPrevDTO> GetBookCollectionsByUser(UserDTO user, int pageNumber = 1, int pageSize = 20)
        {
            FilterDto filter = new()
            {
                Predicate = new PredicateDto("UserId", user.Id, ValueComparingOperator.Equal),
                RequestedPageNumber = pageNumber,
                PageSize = pageSize
            };

            return _queryObject.ExecuteQuery(filter).Items;
        }
    }
}
