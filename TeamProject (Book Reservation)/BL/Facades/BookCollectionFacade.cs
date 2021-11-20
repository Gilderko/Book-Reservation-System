using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookCollection;
using BL.Services;
using DAL.Entities;
using DAL.Entities.ConnectionTables;
using Infrastructure;

namespace BL.Facades
{
    public class BookCollectionFacade
    {
        private IUnitOfWork _unitOfWork;
        private ICRUDService<BookCollectionDTO, BookCollection> _bookCollectionService;
        private ICRUDService<BookCollectionBookDTO, BookCollectionBook> _bookCollectionBookService;

        public BookCollectionFacade(IUnitOfWork unitOfWork,
                                    ICRUDService<BookCollectionDTO, BookCollection> bookCollectionService,
                                    ICRUDService<BookCollectionBookDTO, BookCollectionBook> bookCollectionBookService)
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
            _bookCollectionBookService.Insert(new BookCollectionBookDTO { BookCollect = bookCollection, Book = book });
            _unitOfWork.Commit();
        }

        public void DeleteBookFromCollection(BookCollectionDTO bookCollection, BookDTO book)
        {
            _bookCollectionBookService.Delete(new BookCollectionBookDTO { BookCollect = bookCollection, Book = book });
            _unitOfWork.Commit();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
