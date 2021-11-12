using BL.DTOs.FullVersions;
using BL.Services;
using DAL.Entities;
using Infrastructure;

namespace BL.Facades
{
    public class AuthorFacade
    {
        private IUnitOfWork _unitOfWork;
        private CRUDService<AuthorDTO, Author> _service;

        public AuthorFacade(IUnitOfWork unitOfWork, CRUDService<AuthorDTO, Author> service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public void Create(AuthorDTO author)
        {
            _service.Insert(author);
        }

        public AuthorDTO Get(int id)
        {
            return _service.GetByID(id);
        }

        public void Update(AuthorDTO author)
        {
            _service.Update(author);
        }

        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
