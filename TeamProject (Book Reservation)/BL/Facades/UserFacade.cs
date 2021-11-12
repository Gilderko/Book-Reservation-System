using BL.DTOs.FullVersions;
using BL.Services;
using DAL.Entities;
using Infrastructure;

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
