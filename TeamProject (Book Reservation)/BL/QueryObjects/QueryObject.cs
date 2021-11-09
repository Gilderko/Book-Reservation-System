using AutoMapper;
using BL.DTOs;
using BL.DTOs.Filters;
using DAL.Entities;
using EFInfrastructure;
using Infrastructure;
using Infrastructure.Query.Predicates;

namespace BL.QueryObjects
{
    public class QueryObject<TEntityDTO,TEntity> where TEntity : class, IEntity
                                                 where TEntityDTO : class, IEntityDTO
    {
        private IMapper _mapper;

        private Query<TEntity> _myQuery;

        public QueryObject(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _myQuery = new Query<TEntity>(unitOfWork);
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

            _myQuery.LoadExplicitCollections(filter._collectionsToLoad);
            _myQuery.LoadExplicitReferences(filter._refsToLoad);

            var queryResult = _myQuery.Execute();

            var queryResultDto = _mapper.Map<QueryResultDTO<TEntityDTO>>(queryResult);
            return queryResultDto;
        }
    }
}