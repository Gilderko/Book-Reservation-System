using System.Collections.Generic;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.EReaderInstance;
using BL.DTOs.Entities.Reservation;
using BL.Services;
using DAL.Entities;
using Infrastructure;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class ReservationFacade
    {
        private IUnitOfWork _unitOfWork;
        private IReservationService _service;

        public ReservationFacade(IUnitOfWork unitOfWork, IReservationService service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public async Task Create(ReservationDTO reservation)
        {
            await _service.Insert(reservation);
        }

        public async Task<ReservationDTO> Get(int id, string[] refsToLoad = null, string[] collectToLoad = null)
        {
            return await _service.GetByID(id, refsToLoad, collectToLoad);
        }

        public void Update(ReservationDTO reservation)
        {
            _service.Update(reservation);
        }

        public void Delete(int id)
        {
            _service.DeleteById(id);
        }

        public void AddBookInstance(BookInstanceDTO bookInstance, ReservationDTO reservation)
        {
            ReservationBookInstanceDTO resBookInstance = new ReservationBookInstanceDTO()
            {
                BookInstance = bookInstance,
                Reservation = reservation
            };

            ((List<ReservationBookInstanceDTO>)reservation.BookInstances).Add(resBookInstance);
            _unitOfWork.Commit();
        }

        public void AddEReaderInstance(EReaderInstanceDTO eReaderInstance, ReservationDTO reservation)
        {
            reservation.EReader = eReaderInstance;
            _unitOfWork.Commit();
        }

        public void SubmitReservation(ReservationDTO newReservation)
        {
            // TODO
            // check validity
            
            _service.Insert(newReservation);
        }
    }
}
