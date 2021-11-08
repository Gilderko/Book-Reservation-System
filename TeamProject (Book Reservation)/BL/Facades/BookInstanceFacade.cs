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
    public class BookInstanceFacade
    {
        private IUnitOfWork unitOfWork;
        private CRUDService<BookInstanceDTO, BookInstance> service;

        public BookInstanceFacade()
        {
            unitOfWork = new UnitOfWork(new BookRentalDbContext());
            service = new CRUDService<BookInstanceDTO, BookInstance>(unitOfWork);
        }

        public void Create(BookInstanceDTO bookInstance)
        {
            service.Insert(bookInstance);
        }

        public BookInstanceDTO Get(int id)
        {
            return service.GetByID(id);
        }

        public void Update(BookInstanceDTO bookInstance)
        {
            service.Update(bookInstance);
        }

        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
