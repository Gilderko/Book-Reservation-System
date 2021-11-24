using System.Collections.Generic;
using BL.DTOs.Entities.EReaderInstance;
using BL.Services;
using DAL.Entities;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class EReaderInstancePreviewFacade
    {
        private IEReaderInstancePreviewService _service;
        

        public EReaderInstancePreviewFacade(IEReaderInstancePreviewService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<EReaderInstancePrevDTO>> GetEReaderInstancesByOwner(int ownerId)
        {
            return await _service.GetEReaderInstancesByOwner(ownerId);
        }
    }
}