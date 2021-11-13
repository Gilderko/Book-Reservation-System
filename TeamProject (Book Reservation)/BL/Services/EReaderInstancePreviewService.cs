using System.Collections.Generic;
using AutoMapper;
using BL.DTOs.Filters;
using BL.DTOs.Previews;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure.Query.Operators;

namespace BL.Services
{
    public class EReaderInstancePreviewService
    {
        private IMapper _mapper;
        private QueryObject<EReaderInstancePrevDTO, EReaderInstance> _resQueryObject;
        
        public EReaderInstancePreviewService(IMapper mapper, QueryObject<EReaderInstancePrevDTO, EReaderInstance> resQueryObject)
        {
            _mapper = mapper;
            _resQueryObject = resQueryObject;
        }

        public IEnumerable<EReaderInstancePrevDTO> GetEReaderInstancesByOwner(int ownerId)
        {
            FilterDto filter = new FilterDto()
            {
                Predicate = new PredicateDto("EreaderOwnerId", ownerId, ValueComparingOperator.Equal),
                SortCriteria = "Id",
                SortAscending = true
            };
            
            return _resQueryObject.ExecuteQuery(filter).Items;
        }
    }
}