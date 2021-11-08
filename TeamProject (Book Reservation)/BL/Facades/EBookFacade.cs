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
    public class EBookFacade
    {
        private IUnitOfWork unitOfWork;
        private CRUDService<EBookDTO, EBook> service;

        public EBookFacade()
        {
            unitOfWork = new UnitOfWork(new BookRentalDbContext());
            service = new CRUDService<EBookDTO, EBook>(unitOfWork);
        }

        public void Create(EBookDTO eBook)
        {
            service.Insert(eBook);
        }

        public EBookDTO Get(int id)
        {
            return service.GetByID(id);
        }

        public void Update(EBookDTO eBook)
        {
            service.Update(eBook);
        }

        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
