using BL.DTOs.Entities.EReader;
using BL.Services;
using DAL.Entities;
using Infrastructure;
using System.Threading.Tasks;

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

        public async Task Create(EReaderDTO eReader)
        {
            await _service.Insert(eReader);
            _unitOfWork.Commit();
        }

        public async Task<EReaderDTO> Get(int id, string[] refsToLoad = null, string[] collectToLoad = null)
        {
            return await _service.GetByID(id, refsToLoad, collectToLoad);
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
