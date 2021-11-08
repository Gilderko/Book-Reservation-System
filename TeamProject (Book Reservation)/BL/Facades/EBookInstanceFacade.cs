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
    public class EBookInstanceFacade
    {
        private IUnitOfWork unitOfWork;
        private CRUDService<EBookInstanceDTO, EBookInstance> service;

        public EBookInstanceFacade()
        {
            unitOfWork = new UnitOfWork(new BookRentalDbContext());
            service = new CRUDService<EBookInstanceDTO, EBookInstance>(unitOfWork);
        }

        public void Create(EBookInstanceDTO eBookInstance)
        {
            service.Insert(eBookInstance);
        }

        public EBookInstanceDTO Get(int id)
        {
            return service.GetByID(id);
        }

        public void Update(EBookInstanceDTO eBookInstance)
        {
            service.Update(eBookInstance);
        }

        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
