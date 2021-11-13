using System.Collections.Generic;
using BL.DTOs.ConnectionTables;
using BL.DTOs.FullVersions;
using BL.Services;
using DAL.Entities;
using Infrastructure;

namespace BL.Facades
{
    public class ReservationFacade
    {
        private IUnitOfWork _unitOfWork;
        private CRUDService<ReservationDTO, Reservation> _service;

        public ReservationFacade(IUnitOfWork unitOfWork, CRUDService<ReservationDTO, Reservation> service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public void Create(ReservationDTO reservation)
        {
            _service.Insert(reservation);
        }

        public ReservationDTO Get(int id)
        {
            return _service.GetByID(id);
        }

        public void Update(ReservationDTO reservation)
        {
            _service.Update(reservation);
        }

        public void Delete(int id)
        {
            _service.Delete(id);
        }

        public void AddBookInstance(BookInstanceDTO bookInstance, ReservationDTO reservation)
        {
            using (_unitOfWork)
            {
                Reservation_BookInstanceDTO resBookInstance = new Reservation_BookInstanceDTO()
                {
                    BookInstance = bookInstance,
                    Reservation = reservation
                };
                
                ((List<Reservation_BookInstanceDTO>) reservation.BookInstances).Add(resBookInstance);
                _unitOfWork.Commit();
            }
        }

        public void AddEReaderInstance(EReaderInstanceDTO eReaderInstance, ReservationDTO reservation)
        {
            using (_unitOfWork)
            {
                reservation.EReader = eReaderInstance;
                _unitOfWork.Commit();
            }
        }

        public void SubmitReservation()
        {
            
        }
    }
}
