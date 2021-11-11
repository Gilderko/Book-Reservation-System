using AutoMapper;
using BL.DTOs;
using BL.DTOs.Filters;
using DAL.Entities;
using EFInfrastructure;
using Infrastructure;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using System;

namespace BL.QueryObjects
{
    public class QueryObject<TEntityDTO,TEntity> where TEntity : class, IEntity
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

        public QueryResultDTO<TEntityDTO> ExecuteQuery(FilterDto filter)
        {
            if (filter.Predicate is PredicateDto)
            {
                _myQuery.Where(_mapper.Map<SimplePredicate>(filter.Predicate));   
            }
            else
            {
                _myQuery.Where(_mapper.Map<CompositePredicate>(filter.Predicate));
            }

            if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
            {
                _myQuery.SortBy(filter.SortCriteria, filter.SortAscending);
            }
            if (filter.RequestedPageNumber.HasValue)
            {
                _myQuery.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            var queryResult = _myQuery.Execute();

            var queryResultDto = _mapper.Map<QueryResultDTO<TEntityDTO>>(queryResult);
            return queryResultDto;
        }
    }
}