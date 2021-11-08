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
        private IUnitOfWork unitOfWork;
        private CRUDService<UserDTO, User> service;

        public UserFacade()
        {
            unitOfWork = new UnitOfWork(new BookRentalDbContext());
            service = new CRUDService<UserDTO, User>(unitOfWork);
        }

        public void Create(UserDTO user)
        {
            service.Insert(user);
        }

        public UserDTO Get(int id)
        {
            return service.GetByID(id);
        }

        public void Update(UserDTO user)
        {
            service.Update(user);
        }

        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
