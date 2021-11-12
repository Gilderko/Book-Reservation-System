using BL.DTOs.FullVersions;
using BL.Services;
using DAL.Entities;
using Infrastructure;

namespace BL.Facades
{
    public class BookFacade
    {
        private IUnitOfWork _unitOfWork;
        private CRUDService<BookDTO, Book> _service;

        public BookFacade(IUnitOfWork unitOfWork, CRUDService<BookDTO, Book> service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public void Create(BookDTO book)
        {
            _service.Insert(book);
        }

        public BookDTO Get(int id)
        {
            return _service.GetByID(id);
        }

        public void Update(BookDTO book)
        {
            _service.Update(book);
        }

        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
