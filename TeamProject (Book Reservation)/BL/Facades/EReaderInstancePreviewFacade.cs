using System.Collections.Generic;
using BL.DTOs.Entities.EReaderInstance;
using BL.Services;
using DAL.Entities;

namespace BL.Facades
{
    public class EReaderInstancePreviewFacade
    {
        private IEReaderInstancePreviewService _service;
        

        public EReaderInstancePreviewFacade(IEReaderInstancePreviewService service)
        {
            _service = service;
        }

        public IEnumerable<EReaderInstancePrevDTO> GetEReaderInstancesByOwner(int ownerId)
        {
            return _service.GetEReaderInstancesByOwner(ownerId);
        }
    }
}