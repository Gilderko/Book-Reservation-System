using BL.DTOs.Entities.EReader;
using BL.Services;
using DAL.Entities;
using Infrastructure;

namespace BL.Facades
{
    public class EReaderFacade
    {
        private IUnitOfWork _unitOfWork;
        private ICRUDService<EReaderDTO, EReader> _service;

        public EReaderFacade(IUnitOfWork unitOfWork, ICRUDService<EReaderDTO, EReader> service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public void Create(EReaderDTO eReader)
        {
            _service.Insert(eReader);
            _unitOfWork.Commit();
        }

        public EReaderDTO Get(int id)
        {
            return _service.GetByID(id);
        }

        public void Update(EReaderDTO eReader)
        {
            _service.Update(eReader);
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
