using BL.DTOs.Entities.EReaderInstance;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IEReaderInstancePreviewService : ICRUDService<EReaderInstancePrevDTO, EReaderInstance>
    {
        public Task<IEnumerable<EReaderInstancePrevDTO>> GetEReaderInstancesByOwner(int ownerId);
    }
}