using BL.DTOs.FullVersions;
using BL.Services;
using DAL.Entities;
using Infrastructure;

namespace BL.Facades
{
    public class EReaderFacade
    {
        private IUnitOfWork _unitOfWork;
        private CRUDService<EReaderDTO, EReader> _service;

        public EReaderFacade(IUnitOfWork unitOfWork, CRUDService<EReaderDTO, EReader> service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public void Create(EReaderDTO eReader)
        {
            _service.Insert(eReader);
        }

        public EReaderDTO Get(int id)
        {
            return _service.GetByID(id);
        }

        public void Update(EReaderDTO eReader)
        {
            _service.Update(eReader);
        }

        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
