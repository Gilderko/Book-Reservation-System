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
    public class EReaderInstanceFacade
    {
        private IUnitOfWork unitOfWork;
        private CRUDService<EReaderInstanceDTO, EReaderInstance> service;

        public EReaderInstanceFacade()
        {
            unitOfWork = new UnitOfWork(new BookRentalDbContext());
            service = new CRUDService<EReaderInstanceDTO, EReaderInstance>(unitOfWork);
        }

        public void Create(EReaderInstanceDTO eReaderInstance)
        {
            service.Insert(eReaderInstance);
        }

        public EReaderInstanceDTO Get(int id)
        {
            return service.GetByID(id);
        }

        public void Update(EReaderInstanceDTO eReaderInstance)
        {
            service.Update(eReaderInstance);
        }

        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
