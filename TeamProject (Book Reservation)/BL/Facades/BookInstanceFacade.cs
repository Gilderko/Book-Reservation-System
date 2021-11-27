using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.Reservation;
using BL.DTOs.Entities.User;
using BL.Services;
using DAL.Entities;
using Infrastructure;

namespace BL.Facades
{
    public class BookInstanceFacade
    {
        private IUnitOfWork _unitOfWork;
        private IBookInstanceService _bookInstanceService;
        private ICRUDService<UserPrevDTO, User> _userPrevService;
        private IReservationService _reservationService;
        private IBookInstancePreviewService _bookInstancePreviewService;

        public BookInstanceFacade(IUnitOfWork unitOfWork,
                                  IBookInstanceService bookInstanceService,
                                  IReservationService reservationService,
                                  IBookInstancePreviewService bookInstancePreviewService)
        {
            _unitOfWork = unitOfWork;
            _bookInstanceService = bookInstanceService;
            _reservationService = reservationService;
            _bookInstancePreviewService = bookInstancePreviewService;
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

        public async Task<IEnumerable<ReservationPrevDTO>> GetBookReservationPrevsByUser(int bookInstanceId, DateTime from, DateTime to)
        {
            var reservationPreviews = await _reservationService.GetReservationPrevsByBookInstance(bookInstanceId, from, to);
            
            foreach (var reservationPrev in reservationPreviews)
            {
                var userPrev = await _userPrevService.GetByID(reservationPrev.Id);
                reservationPrev.User = userPrev;
            }

            return reservationPreviews;
        }

        public async Task<IEnumerable<BookInstancePrevDTO>> GetBookInstancePrevsByUser(int userId)
        {
            return await _bookInstancePreviewService.GetBookInstancePrevsByUser(userId);
        }

        public async Task<IEnumerable<BookInstancePrevDTO>> GetAvailableInstancePrevsByDate(BookDTO book, DateTime from,
            DateTime to)
        {
            return await _bookInstancePreviewService.GetAvailableInstancePrevsByDate(book, from, to);
        }

        public async Task CreateUserBookInstance(int ownerId, BookInstanceCreateDTO bookInstance)
        {
            await _bookInstanceService.CreateBookInstance(ownerId, bookInstance);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
