using AutoMapper;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.EBook;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;

namespace BL.Services
{
    public class BookPreviewService : CRUDService<BookPrevDTO, Book>, IBookPreviewService
    {
        private QueryObject<BookPrevDTO, Book> _resQueryObject;

        public BookPreviewService(IRepository<Book> repo, 
            IMapper mapper, 
            QueryObject<BookPrevDTO, Book> resQueryObject) : base(repo, mapper)
        {
            _resQueryObject = resQueryObject;
        }
    }
}
