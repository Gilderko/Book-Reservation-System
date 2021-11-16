using System;
using System.Collections.Generic;
using BL.DTOs.Entities.EBook;
using BL.DTOs.Entities.EReaderInstance;
using BL.DTOs.Entities.Reservation;
using BL.Services;
using DAL.Entities;
using Infrastructure;

namespace BL.Facades
{
    public class EReaderInstanceFacade
    {
        private IUnitOfWork _unitOfWork;
        private EReaderInstanceService<EReaderInstanceDTO, EReaderInstance> _eReaderService;
        private ReservationService<ReservationDTO, Reservation> _reservationService;

        public EReaderInstanceFacade(IUnitOfWork unitOfWork, CRUDService<EReaderInstanceDTO, 
                                     EReaderInstance> crudService, 
                                     EReaderInstanceService<EReaderInstanceDTO, EReaderInstance> eReaderService,
                                     ReservationService<ReservationDTO, Reservation> reservationService)
        {
            _unitOfWork = unitOfWork;
            _eReaderService = eReaderService;
            _reservationService = reservationService;
        }

        public void Create(EReaderInstanceDTO eReaderInstance)
        {
            _eReaderService.Insert(eReaderInstance);
            _unitOfWork.Commit();
        }

        public EReaderInstanceDTO Get(int id)
        {
            return _eReaderService.GetByID(id);
        }

        public void Update(EReaderInstanceDTO eReaderInstance)
        {
            _eReaderService.Update(eReaderInstance);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _eReaderService.Delete(id);
            _unitOfWork.Commit();
        }
        
        public void AddEBook(EReaderInstanceDTO eReaderInstance, EBookDTO eBook)
        {
            _eReaderService.AddEBook(eReaderInstance, eBook);
            _unitOfWork.Commit();
        }
        
        public void DeleteEBook(EReaderInstanceDTO eReaderInstance, EBookDTO eBook)
        {
            _eReaderService.DeleteEBook(eReaderInstance, eBook);
            _unitOfWork.Commit();
        }

        public IEnumerable<ReservationPrevDTO> GetReservationPrevsByDate(EReaderInstanceDTO eReader, DateTime from, DateTime to)
        {
            return _reservationService.GetReservationPrevsByEReader(eReader.Id, from, to);
        }
        
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
