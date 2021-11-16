using System.Collections.Generic;
using BL.DTOs.Entities.EReaderInstance;
using DAL.Entities;

namespace BL.Services
{    
    public interface IEReaderInstancePreviewService : ICRUDService<EReaderInstancePrevDTO, EReaderInstance>
    {
        public IEnumerable<EReaderInstancePrevDTO> GetEReaderInstancesByOwner(int ownerId);
    }
}