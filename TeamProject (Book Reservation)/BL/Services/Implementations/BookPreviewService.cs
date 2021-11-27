using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.EBook;
using BL.DTOs.Filters;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;

namespace BL.Services.Implementations
{
    public class BookPreviewService : CRUDService<BookPrevDTO, Book>, IBookPreviewService
    {
        public BookPreviewService(IRepository<Book> repo, 
            IMapper mapper, 
            QueryObject<BookPrevDTO, Book> resQueryObject) : base(repo, mapper, resQueryObject)
        {

        }

        public async Task<IEnumerable<BookPrevDTO>> GetBookPreviewsByFilter(FilterDto filter)
        {
            string[] collectionsToLoad = new string[]
            {
                nameof(Book.Authors)
            };

            return (await FilterBy(filter,null,collectionsToLoad));
        }
    }
}
