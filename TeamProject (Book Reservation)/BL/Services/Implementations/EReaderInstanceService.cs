using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.EReaderInstance;
using BL.DTOs.Filters;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query.Operators;

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