using BL.DTOs.FullVersions;
using BL.Services;
using DAL;
using DAL.Entities;
using EFInfrastructure;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class BookFacade
    {
        private IUnitOfWork unitOfWork;
        private CRUDService<BookDTO, Book> service;

        public BookFacade()
        {
            unitOfWork = new UnitOfWork(new BookRentalDbContext());
            service = new CRUDService<BookDTO, Book>(unitOfWork);
        }

        public void Create(BookDTO book)
        {
            service.Insert(book);
        }

        public BookDTO Get(int id)
        {
            return service.GetByID(id);
        }

        public void Update(BookDTO book)
        {
            service.Update(book);
        }

        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
