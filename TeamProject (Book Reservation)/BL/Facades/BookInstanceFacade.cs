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
        private ICRUDService<BookInstanceDTO,BookInstance> _service;
        private IReservationService _reservationService;
        private IBookInstancePreviewService _bookInstancePreviewService;

        public BookInstanceFacade(IUnitOfWork unitOfWork,
                                  ICRUDService<BookInstanceDTO, BookInstance> service,
                                  IReservationService reservationService,
                                  IBookInstancePreviewService bookInstancePreviewService)
        {
            _unitOfWork = unitOfWork;
            _service = service;
            _reservationService = reservationService;
            _bookInstancePreviewService = bookInstancePreviewService;
        }
        
        public async Task Create(BookInstanceDTO bookInstance)
        {
            await _service.Insert(bookInstance);
            _unitOfWork.Commit();
        }

        public async Task<BookInstanceDTO> Get(int id)
        {
            return await _service.GetByID(id);
        }

        public void Update(BookInstanceDTO bookInstance)
        {
            _service.Update(bookInstance);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _service.DeleteById(id);
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<ReservationPrevDTO>> GetBookReservationPrevsByUser(int bookInstanceId, DateTime from, DateTime to)
        {
           return await _reservationService.GetReservationPrevsByBookInstance(bookInstanceId, from, to);
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

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
