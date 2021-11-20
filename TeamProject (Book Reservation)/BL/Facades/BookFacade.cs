using BL.Services;
using DAL.Entities;
using Infrastructure;
using System;
using System.Collections.Generic;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookInstance;

namespace BL.Facades
{
    public class BookFacade
    {
        private IUnitOfWork _unitOfWork;
        private ICRUDService<BookDTO, Book> _service;

        public BookFacade(IUnitOfWork unitOfWork,
                          ICRUDService<BookDTO, Book> service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public void Create(BookDTO book)
        {
            _service.Insert(book);
            _unitOfWork.Commit();
        }

        public BookDTO Get(int id)
        {
            return _service.GetByID(id);
        }

        public void Update(BookDTO book)
        {
            _service.Update(book);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _service.Delete(id);
            _unitOfWork.Commit();
        }
        
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
