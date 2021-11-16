using System.Collections.Generic;
using AutoMapper;
using BL.DTOs.Entities.EReaderInstance;
using BL.DTOs.Filters;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query.Operators;

namespace BL.Services
{    
    public class EReaderInstancePreviewService<TEntityDTO, TEntity> : CRUDService<TEntityDTO, TEntity>, 
        IEReaderInstancePreviewService<TEntityDTO, TEntity> where TEntity : EReaderInstance
                                                            where TEntityDTO : EReaderInstancePrevDTO
    {
        private QueryObject<EReaderInstancePrevDTO, EReaderInstance> _resQueryObject;
        
        public EReaderInstancePreviewService(IRepository<TEntity> repo, 
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