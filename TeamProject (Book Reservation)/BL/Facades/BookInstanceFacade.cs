using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.Reservation;
using BL.DTOs.Entities.User;
using BL.DTOs.Filters;
using BL.Services;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query.Operators;

namespace BL.Facades
{
    public class BookInstanceFacade
    {
        private IUnitOfWork _unitOfWork;
        private IBookInstanceService _bookInstanceService;
        private ICRUDService<UserPrevDTO, User> _userPrevService;
        private IReservationService _reservationService;
        private IBookInstancePreviewService _bookInstancePreviewService;
        private IAuthorService _authorService;
        private ICRUDService<BookDTO, Book> _bookService;

        public BookInstanceFacade(IUnitOfWork unitOfWork,
                                  IBookInstanceService bookInstanceService,
                                  IReservationService reservationService,
                                  IBookInstancePreviewService bookInstancePreviewService,
                                  ICRUDService<UserPrevDTO, User> userPrevService,
                                  IAuthorService authorService,
                                  ICRUDService<BookDTO,Book> bookService)
        {
            _unitOfWork = unitOfWork;
            _bookInstanceService = bookInstanceService;
            _reservationService = reservationService;
            _bookInstancePreviewService = bookInstancePreviewService;
            _authorService = authorService;
            _userPrevService = userPrevService;
            _bookService = bookService;
        }

        public async Task<bool> DoesBookExist(int bookId)
        {
            return (await _bookService.GetByID(bookId)) != null;
        }

        public async Task<IEnumerable<BookInstancePrevDTO>> GetAllBookInstances()
        {
            var predicate = new PredicateDto(nameof(BookInstanceDTO.Id), 1, ValueComparingOperator.GreaterThanOrEqual);

            var filter = new FilterDto()
            {
                Predicate = predicate,
                SortAscending = true
            };

            string[] refsToLoad = new string[]
            {
                nameof(BookInstancePrevDTO.FromBookTemplate),
            };

            var result = await _bookInstancePreviewService.FilterBy(filter, refsToLoad);
            await _authorService.LoadAuthors(result.items);

            return result.items;
        }
        
        public async Task Create(BookInstanceDTO bookInstance)
        {
            await _bookInstanceService.Insert(bookInstance);
            _unitOfWork.Commit();
        }

        public async Task<BookInstanceDTO> Get(int id, string[] refsToLoad = null, string[] collectToLoad = null)
        {
            return await _bookInstanceService.GetByID(id, refsToLoad, collectToLoad);
        }

        public void Update(BookInstanceDTO bookInstance)
        {
            _bookInstanceService.Update(bookInstance);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _bookInstanceService.DeleteById(id);
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<ReservationPrevDTO>> GetBookReservationPrevsByBookInstanceAndDate(int bookInstanceId, DateTime? from, DateTime? to)
        {
            var reservationPreviews = await _reservationService.GetReservationPrevsByBookInstance(bookInstanceId, from, to);
            
            foreach (var reservationPrev in reservationPreviews)
            {
                var userPrev = await _userPrevService.GetByID(reservationPrev.UserID);
                reservationPrev.User = userPrev;
            }

            return reservationPreviews;
        }

        public async Task<IEnumerable<BookInstancePrevDTO>> GetBookInstancePrevsByUser(int userId)
        {
            var prevs = await _bookInstancePreviewService.GetBookInstancePrevsByUser(userId);
            await _authorService.LoadAuthors(prevs);

            return prevs;
        }

        public async Task<IEnumerable<BookInstancePrevDTO>> GetAvailableInstancePrevsByDate(BookDTO book, DateTime from,
            DateTime to)
        {
            return await _bookInstancePreviewService.GetAvailableInstancePrevsByDate(book, from, to);
        }

        public async Task CreateUserBookInstance(int ownerId, int bookTemplateId, BookInstanceCreateDTO bookInstance)
        {
            await _bookInstanceService.CreateBookInstance(ownerId, bookTemplateId, bookInstance);
            _unitOfWork.Commit();
        }
    }
}
