using BL.DTOs.Entities.EBook;
using BL.Services;
using DAL.Entities;
using Infrastructure;

namespace BL.Facades
{
    public class EBookFacade
    {
        private IUnitOfWork _unitOfWork;
        private ICRUDService<EBookDTO, EBook> _service;

        public EBookFacade(IUnitOfWork unitOfWork, ICRUDService<EBookDTO, EBook> service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public void Create(EBookDTO eBook)
        {
            _service.Insert(eBook);
            _unitOfWork.Commit();
        }

        public EBookDTO Get(int id)
        {
            return _service.GetByID(id);
        }

        public void Update(EBookDTO eBook)
        {
            _service.Update(eBook);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _service.DeleteById(id);
            _unitOfWork.Commit();
        }
        
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
