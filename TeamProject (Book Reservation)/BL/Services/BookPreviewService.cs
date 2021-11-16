using AutoMapper;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.EBook;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;

namespace BL.Services
{
    public class BookPreviewService<TEntityDTO, TEntity> : CRUDService<TEntityDTO, TEntity>, 
        IBookPreviewService<TEntityDTO, TEntity> where TEntity : Book
                                                 where TEntityDTO : BookPrevDTO
    {
        private QueryObject<BookPrevDTO, Book> _resQueryObject;

        public BookPreviewService(IRepository<TEntity> repo, 
            IMapper mapper, 
            QueryObject<BookPrevDTO, Book> resQueryObject) : base(repo, mapper)
        {
            _resQueryObject = resQueryObject;
        }
    }
}
