using System;
using System.Collections.Generic;
using BL.DTOs.FullVersions;
using BL.DTOs.Previews;
using BL.Services;
using DAL.Entities;
using Infrastructure;

namespace BL.Facades
{
    public class BookInstanceFacade
    {
        private IUnitOfWork _unitOfWork;
        private CRUDService<BookInstanceDTO, BookInstance> _service;
        private ReservationService _reservationService;

        public BookInstanceFacade(IUnitOfWork unitOfWork, 
                                  CRUDService<BookInstanceDTO, BookInstance> service, 
                                  ReservationService reservationService)
        {
            _unitOfWork = unitOfWork;
            _service = service;
            _reservationService = reservationService;
        }

        public void Create(BookInstanceDTO bookInstance)
        {
            _service.Insert(bookInstance);
            _unitOfWork.Commit();
        }

        public BookInstanceDTO Get(int id)
        {
            return _service.GetByID(id);
        }

        public void Update(BookInstanceDTO bookInstance)
        {
            _service.Update(bookInstance);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _service.Delete(id);
            _unitOfWork.Commit();
        }

        public IEnumerable<ReservationPrevDTO> GetBookReservationPrevsByUser(int bookInstanceId, DateTime from, DateTime to)
        {
           return _reservationService.GetReservationPrevsByBookInstance(bookInstanceId, from, to);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
