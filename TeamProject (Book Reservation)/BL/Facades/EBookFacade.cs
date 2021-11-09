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
        private IUnitOfWork _unitOfWork;
        private CRUDService<EBookDTO, EBook> _service;

        public EBookFacade(IUnitOfWork unitOfWork, CRUDService<EBookDTO, EBook> service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public void Create(EBookDTO eBook)
        {
            _service.Insert(eBook);
        }

        public EBookDTO Get(int id)
        {
            return _service.GetByID(id);
        }

        public void Update(EBookDTO eBook)
        {
            _service.Update(eBook);
        }

        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
