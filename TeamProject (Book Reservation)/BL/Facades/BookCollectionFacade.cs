﻿using BL.DTOs.FullVersions;
using BL.Services;
using DAL.Entities;
using Infrastructure;

namespace BL.Facades
{
    public class BookCollectionFacade
    {
        private IUnitOfWork _unitOfWork;
        private CRUDService<BookCollectionDTO, BookCollection> _service;

        public BookCollectionFacade(IUnitOfWork unitOfWork, CRUDService<BookCollectionDTO, BookCollection> service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public void Create(BookCollectionDTO bookCollection)
        {
            _service.Insert(bookCollection);
        }

        public BookCollectionDTO Get(int id)
        {
            return _service.GetByID(id);
        }

        public void Update(BookCollectionDTO bookCollection)
        {
            _service.Update(bookCollection);
        }

        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
