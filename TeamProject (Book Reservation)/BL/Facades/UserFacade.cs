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
    public class UserFacade
    {
        private IUnitOfWork _unitOfWork;
        private CRUDService<UserDTO, User> _service;

        public UserFacade(IUnitOfWork unitOfWork, CRUDService<UserDTO, User> service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public void Create(UserDTO user)
        {
            _service.Insert(user);
        }

        public UserDTO Get(int id)
        {
            return _service.GetByID(id);
        }

        public void Update(UserDTO user)
        {
            _service.Update(user);
        }

        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
