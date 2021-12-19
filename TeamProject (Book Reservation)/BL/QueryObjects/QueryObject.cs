using AutoMapper;
using BL.Config;
using BL.DTOs;
using BL.DTOs.Filters;
using DAL.Entities;
using Infrastructure.Query;
using System;
using System.Threading.Tasks;

namespace BL.QueryObjects
{
    public class QueryObject<TEntityDTO, TEntity> where TEntity : class, IEntity
                                                 where TEntityDTO : class, IEntityDTO
    {
        private IMapper _mapper;

        private IQuery<TEntity> _myQuery;

        public QueryObject(IMapper mapper, IQuery<TEntity> query)
        {
            _mapper = mapper;
            _myQuery = query;
        }

        public void LoadExplicitCollections(Func<TEntity, string[]> collectionsToLoad)
        {
            _myQuery.LoadExplicitCollections(collectionsToLoad.Invoke(null));
        }

        public void LoadExplicitReferences(Func<TEntity, string[]> referencesToLoad)
        {
            _myQuery.LoadExplicitReferences(referencesToLoad.Invoke(null));
        }

        public async Task<QueryResultDTO<TEntityDTO>> ExecuteQuery(FilterDto filter)
        {
            if (filter.Predicate is PredicateDto)
            {
                _myQuery.Where(MappingProfile.ConvertPredicate(filter.Predicate));
            }
            else if (filter.Predicate is CompositePredicateDto)
            {
                _myQuery.Where(MappingProfile.ConvertPredicate(filter.Predicate));
            }

            if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
            {
                _myQuery.SortBy(filter.SortCriteria, filter.SortAscending);
            }
            if (filter.RequestedPageNumber.HasValue)
            {
                _myQuery.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            var queryResult = await _myQuery.Execute();

            var queryResultDto = _mapper.Map<QueryResultDTO<TEntityDTO>>(queryResult);
            return queryResultDto;
        }
    }
}