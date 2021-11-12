using AutoMapper;
using BL.DTOs;
using BL.DTOs.Filters;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure.Query.Operators;
using System;
using System.Collections.Generic;

namespace BL.Services
{
    public class ReservationService
    {
        private IMapper _mapper;
        private QueryObject<ReservationPrevDto, Reservation> _resQueryObject;

        public ReservationService(IMapper mapper, QueryObject<ReservationPrevDto, Reservation> resQueryObject)
        {
            _mapper = mapper;
            _resQueryObject = resQueryObject;
        }

        public IEnumerable<ReservationPrevDto> GetReservationsPreviewByUser(int userId, DateTime from, DateTime to)
        {

            List<PredicateDto> predicates = new List<PredicateDto>
            {
                new PredicateDto("UserID", userId, ValueComparingOperator.Equal),
                new PredicateDto("dateFrom", from.ToString("YYYY-MM-DD"), ValueComparingOperator.GreaterThanOrEqual),
                new PredicateDto("dateTill", to.ToString("YYYY-MM-DD"), ValueComparingOperator.LessThanOrEqual)
            };

            CompositePredicateDto compositePredicate = new CompositePredicateDto(predicates, LogicalOperator.AND);

            FilterDto filter = new FilterDto()
            {
                Predicate = compositePredicate,
                SortCriteria = "dateFrom",
                SortAscending = false
            };

            return _resQueryObject.ExecuteQuery(filter).Items;
        }
    }

    public class ReservationPrevDto : IEntityDTO
    {
    }
}