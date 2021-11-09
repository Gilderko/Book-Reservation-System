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
        private IUnitOfWork _unitOfWork;
        private CRUDService<BookInstanceDTO, BookInstance> _service;

        public BookInstanceFacade(IUnitOfWork unitOfWork, CRUDService<BookInstanceDTO, BookInstance> service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public void Create(BookInstanceDTO bookInstance)
        {
            _service.Insert(bookInstance);
        }

        public BookInstanceDTO Get(int id)
        {
            return _service.GetByID(id);
        }

        public void Update(BookInstanceDTO bookInstance)
        {
            _service.Update(bookInstance);
        }

        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
