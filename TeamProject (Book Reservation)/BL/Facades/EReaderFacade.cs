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
    public class EReaderFacade
    {
        private IUnitOfWork unitOfWork;
        private CRUDService<EReaderDTO, EReader> service;

        public EReaderFacade()
        {
            unitOfWork = new UnitOfWork(new BookRentalDbContext());
            service = new CRUDService<EReaderDTO, EReader>(unitOfWork);
        }

        public void Create(EReaderDTO eReader)
        {
            service.Insert(eReader);
        }

        public EReaderDTO Get(int id)
        {
            return service.GetByID(id);
        }

        public void Update(EReaderDTO eReader)
        {
            service.Update(eReader);
        }

        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
