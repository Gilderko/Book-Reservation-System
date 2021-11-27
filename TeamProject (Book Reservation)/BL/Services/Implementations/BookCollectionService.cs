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
using BL.DTOs.Entities.Book;
using System;

namespace BL.Services.Implementations
{
    public class BookCollectionService : CRUDService<BookCollectionDTO, BookCollection>, IBookCollectionService
    {
        private readonly QueryObject<BookCollectionPrevDTO, BookCollection> _queryObject;

        public BookCollectionService(IRepository<BookCollection> repo, 
                                            IMapper mapper,
                                            QueryObject<BookCollectionDTO, BookCollection> queryObjectBase,
                                            QueryObject<BookCollectionPrevDTO, BookCollection> queryObject) : base(repo, mapper, queryObjectBase)
        {
            _queryObject = queryObject;
        }

        public async Task<IEnumerable<BookCollectionPrevDTO>> GetBookCollectionPrevsByUser(int userId)
        {
            FilterDto filter = new FilterDto()
            {
                Predicate = new PredicateDto(nameof(BookCollectionDTO.UserId), userId, ValueComparingOperator.Equal)
            };

            return (await _queryObject.ExecuteQuery(filter)).Items;
        }

        public async Task CreateUserCollection(BookCollectionCreateDTO bookCollection, int userId)
        {
            bookCollection.CreationDate = DateTime.Now;
            bookCollection.UserId = userId;

            BookCollectionDTO fullBookCollection = Mapper.Map<BookCollectionDTO>(bookCollection);
            await Insert(fullBookCollection);
        }
    }
}
