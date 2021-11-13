using AutoMapper;
using BL.DTOs;
using BL.DTOs.Filters;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure.Query.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using BL.DTOs.ConnectionTables;
using BL.DTOs.FullVersions;
using BL.DTOs.Previews;
using DAL.Entities.ConnectionTables;

namespace BL.Services
{
    public class ReservationService
    {
        private IMapper _mapper;
        private QueryObject<ReservationPrevDTO, Reservation> _resQueryObject;
        private QueryObject<Reservation_BookInstanceDTO, Reservation_BookInstance> _reservationBookInstanceQueryObject;

        public ReservationService(IMapper mapper, 
                                  QueryObject<ReservationPrevDTO, Reservation> resQueryObject,
                                  QueryObject<Reservation_BookInstanceDTO, Reservation_BookInstance> reservationBookInstanceQueryObject)
        {
            _mapper = mapper;
            _resQueryObject = resQueryObject;
            _reservationBookInstanceQueryObject = reservationBookInstanceQueryObject;
        }

        public IEnumerable<ReservationPrevDTO> GetReservationsPreviewByUser(int userId, DateTime from, DateTime to)
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
        
        public IEnumerable<ReservationPrevDTO> GetReservationPrevsByEReader(int eReaderId, DateTime from, DateTime to)
        {
            List<PredicateDto> predicates = new List<PredicateDto>
            {
                new PredicateDto("EReaderID", eReaderId, ValueComparingOperator.Equal),
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
        
        public IEnumerable<ReservationPrevDTO> GetReservationPrevsByBookInstance(int bookId, DateTime from, DateTime to)
        {
            string[] referencesToLoad = new[]
            {
                "Reservation"
            };
                
            _reservationBookInstanceQueryObject.LoadExplicitReferences(instance => referencesToLoad);
            
            FilterDto filter = new FilterDto()
            {
                Predicate = new PredicateDto("BookInstanceID", bookId, ValueComparingOperator.Equal),
                SortCriteria = "ID",
                SortAscending = false,
            };
            
            var result = _reservationBookInstanceQueryObject.ExecuteQuery(filter).Items;
            
            // filter by date
            var reservations = result.
                Where(x => x.Reservation.DateFrom >= from && x.Reservation.DateTill <= to);
            
            return reservations.Select(x => _mapper.Map<ReservationDTO, ReservationPrevDTO>(x.Reservation)).ToList();
        }
    }
}