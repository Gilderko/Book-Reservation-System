using AutoMapper;
using BL.DTOs.Entities.EReaderInstance;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;
using System.Threading.Tasks;

namespace BL.Services.Implementations
{
    public class EReaderInstanceService : CRUDService<EReaderInstanceDTO, EReaderInstance>, IEReaderInstanceService
    {
        public EReaderInstanceService(IRepository<EReaderInstance> repo,
                                      IMapper mapper,
                                      QueryObject<EReaderInstanceDTO, EReaderInstance> resQueryObject) : base(repo, mapper, resQueryObject)
        {

        }

        public async Task AddEReaderInstanceToUser(EReaderInstanceCreateDTO eReaderInstance, int userId)
        {
            eReaderInstance.EreaderOwnerId = userId;
            await Insert(Mapper.Map<EReaderInstanceDTO>(eReaderInstance));
        }
    }
}