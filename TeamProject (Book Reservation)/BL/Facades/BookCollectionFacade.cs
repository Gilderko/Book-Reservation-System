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
    public class BookCollectionFacade
    {
        private IUnitOfWork unitOfWork;
        private CRUDService<BookCollectionDTO, BookCollection> service;

        public BookCollectionFacade()
        {
            unitOfWork = new UnitOfWork(new BookRentalDbContext());
            service = new CRUDService<BookCollectionDTO, BookCollection>(unitOfWork);
        }

        public void Create(BookCollectionDTO bookCollection)
        {
            service.Insert(bookCollection);
        }

        public BookCollectionDTO Get(int id)
        {
            return service.GetByID(id);
        }

        public void Update(BookCollectionDTO bookCollection)
        {
            service.Update(bookCollection);
        }

        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
