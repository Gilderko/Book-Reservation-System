using AutoMapper;
using BL.DTOs.Entities.EReaderInstance;
using BL.DTOs.Filters;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query.Operators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services.Implementations
{
    public class EReaderInstancePreviewService : CRUDService<EReaderInstancePrevDTO, EReaderInstance>, IEReaderInstancePreviewService
    {
        public EReaderInstancePreviewService(IRepository<EReaderInstance> repo,
                                             IMapper mapper,
                                             QueryObject<EReaderInstancePrevDTO, EReaderInstance> resQueryObject) : base(repo, mapper, resQueryObject)
        {

        }

        public async Task<IEnumerable<EReaderInstancePrevDTO>> GetEReaderInstancesByOwner(int ownerId)
        {
            FilterDto filter = new FilterDto()
            {
                Predicate = new PredicateDto(nameof(EReaderInstanceDTO.EreaderOwnerId), ownerId, ValueComparingOperator.Equal),
                SortCriteria = "Id",
                SortAscending = true
            };

            string[] refsToLoad = new string[]
            {
                nameof(EReaderInstance.EReaderTemplate)
            };

            var result = await FilterBy(filter, refsToLoad, null);

            return result.items;
        }
    }
}