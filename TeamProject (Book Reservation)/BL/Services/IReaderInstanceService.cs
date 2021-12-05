using BL.DTOs.Entities.EReaderInstance;
using DAL.Entities;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IEReaderInstanceService : ICRUDService<EReaderInstanceDTO, EReaderInstance>
    {
        public Task AddEReaderInstanceToUser(EReaderInstanceCreateDTO eReaderInstance, int userId);
    }
}