using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.Reservation;
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

        public BookInstanceFacade(IUnitOfWork unitOfWork,
                                  ICRUDService<BookInstanceDTO, BookInstance> service,
                                  IReservationService reservationService)
        {
            _unitOfWork = unitOfWork;
            _service = service;
            _reservationService = reservationService;
        }
        
        public async Task Create(BookInstanceDTO bookInstance)
        {
            await _service.Insert(bookInstance);
            _unitOfWork.Commit();
        }

        public async Task<BookInstanceDTO> Get(int id, string[] refsToLoad = null, string[] collectToLoad = null)
        {
            return await _service.GetByID(id, refsToLoad, collectToLoad);
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

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
