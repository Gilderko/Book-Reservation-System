using System;
using System.Collections;
using System.Collections.Generic;
using BL.DTOs.FullVersions;
using BL.DTOs.Previews;
using BL.Services;
using DAL.Entities;
using Infrastructure;

namespace BL.Facades
{
    public class EReaderInstanceFacade
    {
        private IUnitOfWork _unitOfWork;
        private CRUDService<EReaderInstanceDTO, EReaderInstance> _crudService;
        private EReaderInstanceService _eReaderService;
        private ReservationService _reservationService;

        public EReaderInstanceFacade(IUnitOfWork unitOfWork, CRUDService<EReaderInstanceDTO, 
                                     EReaderInstance> crudService, 
                                     EReaderInstanceService eReaderService,
                                     ReservationService reservationService)
        {
            _unitOfWork = unitOfWork;
            _crudService = crudService;
            _eReaderService = eReaderService;
            _reservationService = reservationService;
        }

        public void Create(EReaderInstanceDTO eReaderInstance)
        {
            _crudService.Insert(eReaderInstance);
            _unitOfWork.Commit();
        }

        public EReaderInstanceDTO Get(int id)
        {
            return _crudService.GetByID(id);
        }

        public void Update(EReaderInstanceDTO eReaderInstance)
        {
            _crudService.Update(eReaderInstance);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _crudService.Delete(id);
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
