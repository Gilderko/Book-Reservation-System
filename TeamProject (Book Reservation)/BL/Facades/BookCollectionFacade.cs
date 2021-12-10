using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookCollection;
using BL.DTOs.Filters;
using BL.Services;
using DAL.Entities;
using DAL.Entities.ConnectionTables;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Query.Operators;
using System.Linq;

namespace BL.Facades
{
    public class BookCollectionFacade
    {
        private IUnitOfWork _unitOfWork;
        private IBookCollectionService _bookCollectionService;
        private ICRUDService<BookCollectionBookDTO, BookCollectionBook> _bookCollectionBookService;
        private IAuthorService _authorService;
        private ICRUDService<BookPrevDTO, Book> _bookPrevService;

        public BookCollectionFacade(IUnitOfWork unitOfWork,
                                    IBookCollectionService bookCollectionService,
                                    ICRUDService<BookCollectionBookDTO, BookCollectionBook> bookCollectionBookService,
                                    IAuthorService authorService,
                                    ICRUDService<BookPrevDTO, Book> bookPrevService)
        {
            _unitOfWork = unitOfWork;
            _bookCollectionService = bookCollectionService;
            _bookCollectionBookService = bookCollectionBookService;
            _authorService = authorService;
            _bookPrevService = bookPrevService;
        }

        public async Task<bool> DoesBookExist(int bookId)
        {
            return (await _bookPrevService.GetByID(bookId)) != null;
        }

        public async Task<(IEnumerable<BookCollectionDTO>,int)> GetAllBookCollections()
        {
            var simplePredicate = new PredicateDto(nameof(BookCollectionDTO.Id), 1, ValueComparingOperator.GreaterThanOrEqual);

            var filter = new FilterDto()
            {
                Predicate = simplePredicate
            };

            return await _bookCollectionService.FilterBy(filter);
        }

        public async Task Create(BookCollectionDTO bookCollection)
        {
            await _bookCollectionService.Insert(bookCollection);
            _unitOfWork.Commit();
        }

        public async Task<BookCollectionDTO> Get(int id, string[] refsToLoad = null, string[] collectToLoad = null)
        {
            return await _bookCollectionService.GetByID(id, refsToLoad, collectToLoad);
        }

        public void Update(BookCollectionDTO bookCollection)
        {
            _bookCollectionService.Update(bookCollection);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _bookCollectionService.DeleteById(id);
            _unitOfWork.Commit();
        }

        public async Task<BookCollectionCreateDTO> GetUserCollectionToEdit(int id)
        {
            return await _bookCollectionService.GetUserCollectionToEdit(id);
        }

        public void UserEditCollection(BookCollectionCreateDTO collection)
        {
            _bookCollectionService.EditUserCollection(collection);
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<BookCollectionPrevDTO>> GetBookCollectionPrevsByUser(int id)
        {
            return await _bookCollectionService.GetBookCollectionPrevsByUser(id);
        }

        public async Task AddBookToCollection(AddBookInCollectionDTO bookCollectionDTO)
        {
            var predicates = new List<PredicateDto>()
            {
                new PredicateDto(nameof(BookCollectionBookDTO.BookID),bookCollectionDTO.BookID,ValueComparingOperator.Equal),
                new PredicateDto(nameof(BookCollectionBookDTO.BookCollectionID),bookCollectionDTO.BookCollectionID,ValueComparingOperator.Equal),
            };

            var compPredicate = new CompositePredicateDto(predicates, LogicalOperator.AND);
            var filter = new FilterDto()
            {
                Predicate = compPredicate
            };

            if ((await _bookCollectionBookService.FilterBy(filter)).items.Count() == 0)
            {
                await _bookCollectionBookService.Insert(new BookCollectionBookDTO
                {
                    BookCollectionID = bookCollectionDTO.BookCollectionID,
                    BookID = bookCollectionDTO.BookID
                });

                _unitOfWork.Commit();
            }
        }

        public void DeleteBookFromCollection(BookCollectionDTO bookCollection, BookDTO book)
        {
            _bookCollectionBookService.Delete(new BookCollectionBookDTO
            {
                BookCollect = bookCollection,
                BookCollectionID = bookCollection.Id,

                Book = book,
                BookID = book.Id
            });
            _unitOfWork.Commit();
        }

        public void DeleteBookFromCollection(int bookCollectionId, int bookId)
        {
            _bookCollectionBookService.Delete(new BookCollectionBookDTO
            {
                BookCollectionID = bookCollectionId,

                BookID = bookId
            });
            _unitOfWork.Commit();
        }

        public async Task CreateUserCollection(BookCollectionCreateDTO bookCollection, int userId)
        {
            await _bookCollectionService.CreateUserCollection(bookCollection, userId);
            _unitOfWork.Commit();
        }

        public async Task<BookCollectionDTO> GetBookCollectionWithBooksAndOwner(int bookCollectionId)
        {
            string[] collectionsToLoad = new string[]
            {
                nameof(BookCollectionDTO.Books)
            };

            string[] refsToLoad = new string[]
            {
                nameof(BookCollectionDTO.OwnerUser)
            };

            return await _bookCollectionService.GetByID(bookCollectionId, refsToLoad, collectionsToLoad);
        }

        public async Task<IEnumerable<BookPrevDTO>> GetBookPreviewsWithAuthorsForCollection(BookCollectionDTO bookCollection)
        {
            HashSet<int> bookIds = new();

            foreach (var book in bookCollection.Books)
            {
                bookIds.Add(book.BookID);
            }

            string[] collectionsToLoad = new string[]
            {
                nameof(Book.Authors)
            };

            FilterDto filter = new FilterDto()
            {
                Predicate = new PredicateDto(nameof(Book.Id), bookIds, ValueComparingOperator.In)
            };

            var books = await _bookPrevService.FilterBy(filter, null, collectionsToLoad);

            await _authorService.LoadAuthors(books.items);

            return books.items;
        }
    }
}
