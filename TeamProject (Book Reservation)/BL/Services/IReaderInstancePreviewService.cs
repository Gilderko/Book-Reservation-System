using System.Collections.Generic;
using BL.DTOs.Entities.EReaderInstance;
using DAL.Entities;

namespace BL.Services
{    
    public interface IEReaderInstancePreviewService<TEntityDTO, TEntity> : ICRUDService<TEntityDTO, TEntity> where TEntity : EReaderInstance
                                                                                                       where TEntityDTO : EReaderInstancePrevDTO
    {
        public IEnumerable<EReaderInstancePrevDTO> GetEReaderInstancesByOwner(int ownerId);
    }
}