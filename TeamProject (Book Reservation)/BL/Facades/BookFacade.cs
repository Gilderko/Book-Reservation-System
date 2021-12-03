using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.User;
using BL.DTOs.Enums;
using BL.DTOs.Filters;
using BL.Services;
using DAL.Entities;
using DAL.Entities.ConnectionTables;
using Infrastructure;
using Infrastructure.Query.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class BookFacade
    {
        private IUnitOfWork _unitOfWork;
        private ICRUDService<BookDTO, Book> _bookService;
        private ICRUDService<BookPrevDTO, Book> _bookPrevService;
        private ICRUDService<AuthorBookDTO, AuthorBook> _authorBookService;
        private ICRUDService<UserDTO, User> _userService;
        private ICRUDService<ReservationBookInstanceDTO, ReservationBookInstance> _reserveBookInstance;
        private IAuthorService _authorService;
        private IGenreService _genreService;

        public BookFacade(IUnitOfWork unitOfWork,
                          ICRUDService<BookDTO, Book> bookService,
                          ICRUDService<BookPrevDTO, Book> bookPrevService,
                          ICRUDService<AuthorBookDTO, AuthorBook> authorBookService,
                          ICRUDService<UserDTO, User> userService,
                          ICRUDService<ReservationBookInstanceDTO, ReservationBookInstance> reserveBookInstance,
                          IAuthorService authorService,
                          IGenreService genreService)
        {
            _unitOfWork = unitOfWork;
            _bookService = bookService;
            _bookPrevService = bookPrevService;
            _authorBookService = authorBookService;
            _userService = userService;
            _reserveBookInstance = reserveBookInstance;
            _authorService = authorService;
            _genreService = genreService;
        }

        public async Task Create(BookDTO book)
        {
            await _bookService.Insert(book);
            _unitOfWork.Commit();
        }

        public async Task<BookDTO> Get(int id, string[] refsToLoad = null, string[] collectToLoad = null)
        {
            var book = await _bookService.GetByID(id, refsToLoad, collectToLoad);

            if (collectToLoad is not null && collectToLoad.Contains(nameof(BookDTO.Reviews)))
            {
                foreach (var review in book.Reviews)
                {
                    review.User = await _userService.GetByID(review.UserID);
                }
            }

            if (collectToLoad is not null && collectToLoad.Contains(nameof(BookDTO.BookInstances)))
            {
                var refsReservBookInstanceToLoad = new string[]
                {
                    nameof(ReservationBookInstance.Reservation)
                };

                foreach (var bookInstance in book.BookInstances)
                {   
                    bookInstance.Owner = await _userService.GetByID(bookInstance.BookOwnerId);

                    var predicate = new PredicateDto(nameof(ReservationBookInstanceDTO.BookInstanceID), bookInstance.Id, ValueComparingOperator.Equal);
                    var filter = new FilterDto()
                    {
                        Predicate = predicate
                    };

                    bookInstance.AllReservations = (await _reserveBookInstance.FilterBy(filter, refsReservBookInstanceToLoad)).ToList();
                }
            }

            return book;
        }

        public void Update(BookDTO book)
        {
            _bookService.Update(book);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _bookService.DeleteById(id);
            _unitOfWork.Commit();
        }
        
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public async Task<IEnumerable<BookPrevDTO>> GetBookPreviews(string title,
                                                                    string authorName,
                                                                    string authorSurname,
                                                                    GenreTypeDTO[] genres,
                                                                    LanguageDTO? language,
                                                                    int? pageFrom,
                                                                    int? pageTo,
                                                                    DateTime? releaseFrom,
                                                                    DateTime? releaseTo)
        {
            FilterDto filter = new FilterDto();

            List<PredicateDto> predicates = new();

            if (title is not null)
            {
                predicates.Add(new PredicateDto(nameof(Book.Title), title, ValueComparingOperator.Contains));
            }

            if (authorName is not null || authorSurname is not null)
            {
                var bookIds = await _authorService.GetAuthorsBooksIdsByName(authorName, authorSurname);
                predicates.Add(new PredicateDto(nameof(Book.Id), bookIds, ValueComparingOperator.In));
            }

            if (genres is not null && genres.Length > 0)
            {
                var bookIds = await _genreService.GetBookIdsByGenres(genres);
                predicates.Add(new PredicateDto(nameof(Book.Id), bookIds, ValueComparingOperator.In));
            }

            if (language is not null)
            {
                predicates.Add(new PredicateDto(nameof(Book.Language), (int)language, ValueComparingOperator.Contains));
            }

            if (pageFrom is not null)
            {
                predicates.Add(new PredicateDto(nameof(Book.PageCount), (int)pageFrom, ValueComparingOperator.GreaterThanOrEqual));
            }

            if (pageTo is not null)
            {
                predicates.Add(new PredicateDto(nameof(Book.PageCount), (int)pageTo, ValueComparingOperator.LessThanOrEqual));
            }

            if (releaseFrom is not null)
            {
                predicates.Add(new PredicateDto(nameof(Book.DateOfRelease), releaseFrom, ValueComparingOperator.GreaterThanOrEqual));
            }

            if (releaseTo is not null)
            {
                predicates.Add(new PredicateDto(nameof(Book.DateOfRelease), releaseTo, ValueComparingOperator.LessThanOrEqual));
            }

            if (predicates.Count > 0)
            {
                filter.Predicate = new CompositePredicateDto(predicates, LogicalOperator.AND);
            }

            string[] collectionsToLoad = new string[] {
                nameof(Book.Authors)
            };

            var previews = await _bookPrevService.FilterBy(filter, null, collectionsToLoad);
            await _authorService.LoadAuthors(previews);

            return previews;
        }

        public async Task<IEnumerable<AuthorBookDTO>> GetAuthorBooksByBook(int id)
        {
            var filter = new FilterDto();
            filter.Predicate = new PredicateDto(nameof(AuthorBookDTO.BookID), id, ValueComparingOperator.Equal);

            var refsToLoad = new[]
            {
                nameof(AuthorBookDTO.Author)
            };

            var result = await _authorBookService.FilterBy(filter, refsToLoad);
            return result;
        }
    }
}
