using System.Collections.Generic;
using System.Threading.Tasks;
using BL.DTOs.Entities.EReaderInstance;
using DAL.Entities;

namespace BL.Services
{    
    public interface IEReaderInstanceService : ICRUDService<EReaderInstanceDTO, EReaderInstance>
    {
        public Task AddEReaderInstanceToUser(EReaderInstanceCreateDTO eReaderInstance, int userId);
    }
}