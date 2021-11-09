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
        private IUnitOfWork _unitOfWork;
        private CRUDService<EBookInstanceDTO, EBookInstance> _service;

        public EBookInstanceFacade(IUnitOfWork unitOfWork, CRUDService<EBookInstanceDTO, EBookInstance> service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public void Create(EBookInstanceDTO eBookInstance)
        {
            _service.Insert(eBookInstance);
        }

        public EBookInstanceDTO Get(int id)
        {
            return _service.GetByID(id);
        }

        public void Update(EBookInstanceDTO eBookInstance)
        {
            _service.Update(eBookInstance);
        }

        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
