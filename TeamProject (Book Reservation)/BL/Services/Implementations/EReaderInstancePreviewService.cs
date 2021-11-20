using System.Collections.Generic;
using AutoMapper;
using BL.DTOs.Entities.EReaderInstance;
using BL.DTOs.Filters;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query.Operators;

namespace BL.Services.Implementations
{    
    public class EReaderInstancePreviewService : CRUDService<EReaderInstancePrevDTO, EReaderInstance>, 
        IEReaderInstancePreviewService
    {
        private QueryObject<EReaderInstancePrevDTO, EReaderInstance> _resQueryObject;

        public EReaderInstancePreviewService(IRepository<EReaderInstance> repo, 
                                             IMapper mapper,
                                             QueryObject<EReaderInstancePrevDTO, EReaderInstance> resQueryObject) : base(repo, mapper)
        {
            _resQueryObject = resQueryObject;               
        }

        public IEnumerable<EReaderInstancePrevDTO> GetEReaderInstancesByOwner(int ownerId)
        {
            FilterDto filter = new FilterDto()
            {
                Predicate = new PredicateDto(nameof(EReaderInstanceDTO.EreaderOwnerId), ownerId, ValueComparingOperator.Equal),
                SortCriteria = "Id",
                SortAscending = true
            };
            
            return _resQueryObject.ExecuteQuery(filter).Items;
        }
    }
}