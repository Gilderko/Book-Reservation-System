using AutoMapper;
using BL.DTOs;
using BL.DTOs.Filters;
using DAL.Entities;
using EFInfrastructure;
using Infrastructure;
using Infrastructure.Query.Predicates;

namespace BL.QueryObjects
{
    public class QueryObject<TEntity> where TEntity : class, IEntity
    {
        private IMapper _mapper;

        private Query<IEntity> _myQuery;

        public QueryObject(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _myQuery = new Query<IEntity>(unitOfWork);
        }

        public QueryResultDTO<IEntityDTO> ExecuteQuery(Filter filter)
        {
            _myQuery.Where(_mapper.Map<CompositePredicate>(filter));
            
            if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
            {
                _myQuery.SortBy(filter.SortCriteria, filter.SortAscending);
            }
            if (filter.RequestedPageNumber.HasValue)
            {
                _myQuery.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }
            var queryResult = _myQuery.Execute();

            var queryResultDto = _mapper.Map<QueryResultDTO<IEntityDTO>>(queryResult);
            return queryResultDto;
        }
    }
}