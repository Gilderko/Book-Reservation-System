using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private ICRUDService<UserPrevDTO, User> _userPrevService;
        private IReservationService _reservationService;

        public BookInstanceFacade(IUnitOfWork unitOfWork,
                                  ICRUDService<BookInstanceDTO, BookInstance> service,
                                  ICRUDService<UserPrevDTO, User> userPrevService,
                                  IReservationService reservationService)
        {
            _unitOfWork = unitOfWork;
            _service = service;
            _userPrevService = userPrevService;
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
            var reservationPreviews = await _reservationService.GetReservationPrevsByBookInstance(bookInstanceId, from, to);
            
            foreach (var reservationPrev in reservationPreviews)
            {
                var userPrev = await _userPrevService.GetByID(reservationPrev.Id);
                reservationPrev.User = userPrev;
            }

            return reservationPreviews;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
