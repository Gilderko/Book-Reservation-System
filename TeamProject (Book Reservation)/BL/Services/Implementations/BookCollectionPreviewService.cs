using AutoMapper;
using BL.DTOs.Filters;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query.Operators;
using System.Collections.Generic;
using BL.DTOs.Entities.BookCollection;
using BL.DTOs.Entities.User;
using System.Threading.Tasks;

namespace BL.Services.Implementations
{
    public class BookCollectionPreviewService : CRUDService<BookCollectionPrevDTO, BookCollection>, 
        IBookCollectionPreviewService
    {
        private readonly QueryObject<BookCollectionPrevDTO, BookCollection> _queryObject;

        public BookCollectionPreviewService(IRepository<BookCollection> repo, 
                                            IMapper mapper, 
                                            QueryObject<BookCollectionPrevDTO, BookCollection> queryObject) : base(repo, mapper)
        {
            _queryObject = queryObject;
        }

        public async Task<IEnumerable<BookCollectionPrevDTO>> GetBookCollectionsByUser(UserDTO user, int pageNumber = 1, int pageSize = 20)
        {
            FilterDto filter = new FilterDto()
            {
                Predicate = new PredicateDto(nameof(BookCollectionDTO.UserId), user.Id, ValueComparingOperator.Equal),
                RequestedPageNumber = pageNumber,
                PageSize = pageSize
            };

            return (await _queryObject.ExecuteQuery(filter)).Items;
        }
    }
}
