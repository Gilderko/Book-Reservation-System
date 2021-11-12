﻿using BL.DTOs.FullVersions;
using BL.Services;
using DAL.Entities;
using Infrastructure;

namespace BL.Facades
{
    public class BookInstanceFacade
    {
        private IUnitOfWork _unitOfWork;
        private CRUDService<BookInstanceDTO, BookInstance> _service;

        public BookInstanceFacade(IUnitOfWork unitOfWork, CRUDService<BookInstanceDTO, BookInstance> service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public void Create(BookInstanceDTO bookInstance)
        {
            _service.Insert(bookInstance);
        }

        public BookInstanceDTO Get(int id)
        {
            return _service.GetByID(id);
        }

        public void Update(BookInstanceDTO bookInstance)
        {
            _service.Update(bookInstance);
        }

        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
