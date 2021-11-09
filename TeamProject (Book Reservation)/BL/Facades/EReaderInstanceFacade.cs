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
        private IUnitOfWork _unitOfWork;
        private CRUDService<EReaderInstanceDTO, EReaderInstance> _service;

        public EReaderInstanceFacade(IUnitOfWork unitOfWork, CRUDService<EReaderInstanceDTO, EReaderInstance> service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public void Create(EReaderInstanceDTO eReaderInstance)
        {
            _service.Insert(eReaderInstance);
        }

        public EReaderInstanceDTO Get(int id)
        {
            return _service.GetByID(id);
        }

        public void Update(EReaderInstanceDTO eReaderInstance)
        {
            _service.Update(eReaderInstance);
        }

        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
