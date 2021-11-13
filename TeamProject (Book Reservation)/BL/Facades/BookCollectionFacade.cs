using BL.DTOs.ConnectionTables;
using BL.DTOs.FullVersions;
using BL.Services;
using DAL.Entities;
using Infrastructure;

namespace BL.Facades
{
    public class BookCollectionFacade
    {
        private IUnitOfWork _unitOfWork;
        private CRUDService<BookCollectionDTO, BookCollection> _service;

        public BookCollectionFacade(IUnitOfWork unitOfWork, CRUDService<BookCollectionDTO, BookCollection> service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public void Create(BookCollectionDTO bookCollection)
        {
            _service.Insert(bookCollection);
            _unitOfWork.Commit();
        }

        public BookCollectionDTO Get(int id)
        {
            return _service.GetByID(id);
        }

        public void Update(BookCollectionDTO bookCollection)
        {
            _service.Update(bookCollection);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _service.Delete(id);
            _unitOfWork.Commit();
        }

        public void AddBookToCollection(BookCollectionDTO bookCollection, BookDTO book)
        {
            bookCollection.Books.Add(new BookCollection_BookDTO { BookCollect = bookCollection, Book = book });
            _service.Update(bookCollection);
            _unitOfWork.Commit();
        }

        public void DeleteBookFromCollection(BookCollectionDTO bookCollection, BookDTO book)
        {
            bookCollection.Books.Remove(new BookCollection_BookDTO { BookCollect = bookCollection, Book = book });
            _service.Update(bookCollection);
            _unitOfWork.Commit();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
