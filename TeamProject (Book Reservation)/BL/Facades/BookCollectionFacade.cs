using BL.DTOs.ConnectionTables;
using BL.DTOs.FullVersions;
using BL.Services;
using DAL.Entities;
using DAL.Entities.ConnectionTables;
using Infrastructure;

namespace BL.Facades
{
    public class BookCollectionFacade
    {
        private IUnitOfWork _unitOfWork;
        private CRUDService<BookCollectionDTO, BookCollection> _bookCollectionService;
        private CRUDService<BookCollection_BookDTO, BookCollection_Book> _bookCollectionBookService;

        public BookCollectionFacade(IUnitOfWork unitOfWork, 
                                    CRUDService<BookCollectionDTO, BookCollection> bookCollectionService,
                                    CRUDService<BookCollection_BookDTO, BookCollection_Book> bookCollectionBookService)
        {
            _unitOfWork = unitOfWork;
            _bookCollectionService = bookCollectionService;
            _bookCollectionBookService = bookCollectionBookService;
        }

        public void Create(BookCollectionDTO bookCollection)
        {
            _bookCollectionService.Insert(bookCollection);
            _unitOfWork.Commit();
        }

        public BookCollectionDTO Get(int id)
        {
            return _bookCollectionService.GetByID(id);
        }

        public void Update(BookCollectionDTO bookCollection)
        {
            _bookCollectionService.Update(bookCollection);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _bookCollectionService.Delete(id);
            _unitOfWork.Commit();
        }

        public void AddBookToCollection(BookCollectionDTO bookCollection, BookDTO book)
        {
            _bookCollectionBookService.Insert(new BookCollection_BookDTO { BookCollect = bookCollection, Book = book });
            _unitOfWork.Commit();
        }

        public void DeleteBookFromCollection(BookCollectionDTO bookCollection, BookDTO book)
        {
            _bookCollectionBookService.Delete(new BookCollection_BookDTO { BookCollect = bookCollection, Book = book });
            _unitOfWork.Commit();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
