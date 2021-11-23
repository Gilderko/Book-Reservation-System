﻿using System;
using System.Collections.Generic;
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
            _service.DeleteById(id);
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
