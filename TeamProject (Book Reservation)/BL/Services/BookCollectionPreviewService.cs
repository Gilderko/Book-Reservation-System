using AutoMapper;
using BL.DTOs.Filters;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query.Operators;
using System.Collections.Generic;
using BL.DTOs.Entities.BookCollection;
using BL.DTOs.Entities.User;

namespace BL.Services
{
    public class BookCollectionPreviewService : CRUDService<BookCollectionPrevDTO, BookCollection>, 
        IBookCollectionPreviewService
    {
        private readonly IRepository<BookCollection> _repository;
        private readonly IMapper _mapper;
        private readonly QueryObject<BookCollectionPrevDTO, BookCollection> _queryObject;

        public BookCollectionPreviewService(IRepository<BookCollection> repo, 
                                            IMapper mapper, 
                                            QueryObject<BookCollectionPrevDTO, BookCollection> queryObject) : base(repo, mapper)
        {
            _queryObject = queryObject;
        }

        public IEnumerable<BookCollectionPrevDTO> GetBookCollectionsByUser(UserDTO user, int pageNumber = 1, int pageSize = 20)
        {
            FilterDto filter = new FilterDto()
            {
                Predicate = new PredicateDto(nameof(BookCollectionDTO.UserId), user.Id, ValueComparingOperator.Equal),
                RequestedPageNumber = pageNumber,
                PageSize = pageSize
            };

            return _queryObject.ExecuteQuery(filter).Items;
        }
    }
}
