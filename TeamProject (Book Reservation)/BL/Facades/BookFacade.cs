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
        private CRUDService<BookDTO, Book> _service;
        private BookInstanceService<BookInstanceDTO, BookInstance> _bookInstanceService;

        public BookFacade(IUnitOfWork unitOfWork,
                          CRUDService<BookDTO, Book> service, 
                          BookInstanceService<BookInstanceDTO, BookInstance> bookInstanceService)
        {
            _unitOfWork = unitOfWork;
            _service = service;
            _bookInstanceService = bookInstanceService;
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

        public IEnumerable<BookInstanceDTO> GetBookInstancesByDate(BookDTO book, DateTime from, DateTime to, int pageNumber, int pageSize)
        {
            return _bookInstanceService.GetBookInstancesByDate(book, from, to, pageNumber, pageSize);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
