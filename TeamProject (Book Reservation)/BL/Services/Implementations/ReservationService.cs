using AutoMapper;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Reservation;
using BL.DTOs.Filters;
using BL.QueryObjects;
using DAL.Entities;
using DAL.Entities.ConnectionTables;
using Infrastructure;
using Infrastructure.Query.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Services.Implementations
{
    public class ReservationService : CRUDService<ReservationDTO, Reservation>, IReservationService
    {
        private QueryObject<ReservationDTO, Reservation> _baseResQueryObject;
        private QueryObject<ReservationPrevDTO, Reservation> _resQueryObject;
        private QueryObject<ReservationBookInstanceDTO, ReservationBookInstance> _reservationBookInstanceQueryObject;

        public ReservationService(IRepository<Reservation> repo,
                                  IMapper mapper,
                                  QueryObject<ReservationDTO, Reservation> baseQueryObject,
                                  QueryObject<ReservationPrevDTO, Reservation> resQueryObject,
                                  QueryObject<ReservationBookInstanceDTO, ReservationBookInstance> reservationBookInstanceQueryObject) : base(repo, mapper, baseQueryObject)
        {
            _baseResQueryObject = baseQueryObject;
            _resQueryObject = resQueryObject;
            _reservationBookInstanceQueryObject = reservationBookInstanceQueryObject;
        }

        public async Task<IEnumerable<ReservationPrevDTO>> GetReservationsPreviewByUser(int userId, DateTime from, DateTime to)
        {
            List<PredicateDto> predicates = new List<PredicateDto>
            {
                new PredicateDto(nameof(Reservation.UserID), userId, ValueComparingOperator.Equal),
                new PredicateDto(nameof(Reservation.DateFrom), from, ValueComparingOperator.GreaterThanOrEqual),
                new PredicateDto(nameof(Reservation.DateTill), to, ValueComparingOperator.LessThanOrEqual)
            };

            CompositePredicateDto compositePredicate = new CompositePredicateDto(predicates, LogicalOperator.AND);

            FilterDto filter = new FilterDto()
            {
                Predicate = compositePredicate,
                SortCriteria = nameof(Reservation.DateFrom),
                SortAscending = false
            };

            return (await _resQueryObject.ExecuteQuery(filter)).Items;
        }

        public async Task<IEnumerable<ReservationPrevDTO>> GetReservationPrevsByEReader(int eReaderId, DateTime? from, DateTime? to)
        {
            List<PredicateDto> predicates = new List<PredicateDto>
            {
                new PredicateDto(nameof(Reservation.EReaderID), eReaderId, ValueComparingOperator.Equal)
            };

            if (from != null)
            {
                predicates.Add(new PredicateDto(nameof(Reservation.DateFrom), from, ValueComparingOperator.GreaterThanOrEqual));
            }

            if (to != null)
            {
                predicates.Add(new PredicateDto(nameof(Reservation.DateTill), to, ValueComparingOperator.LessThanOrEqual));
            }

            CompositePredicateDto compositePredicate = new CompositePredicateDto(predicates, LogicalOperator.AND);

            FilterDto filter = new FilterDto()
            {
                Predicate = compositePredicate,
                SortCriteria = nameof(Reservation.DateFrom),
                SortAscending = false
            };

            return (await _resQueryObject.ExecuteQuery(filter)).Items;
        }

        public async Task<IEnumerable<ReservationPrevDTO>> GetReservationPrevsByBookInstance(int bookId, DateTime? from, DateTime? to)
        {
            string[] referencesToLoad = new[]
            {
                nameof(ReservationBookInstanceDTO.Reservation)
            };

            _reservationBookInstanceQueryObject.LoadExplicitReferences(instance => referencesToLoad);

            FilterDto filter = new FilterDto()
            {
                Predicate = new PredicateDto(nameof(ReservationBookInstanceDTO.BookInstanceID), bookId, ValueComparingOperator.Equal),
                SortCriteria = nameof(ReservationBookInstanceDTO.ReservationID),
                SortAscending = false,
            };

            var result = (await _reservationBookInstanceQueryObject.ExecuteQuery(filter)).Items;

            var reservations = result;

            // Filter by date
            if (from != null && to != null)
            {
                return result.Where(x => x.Reservation.DateFrom >= from && x.Reservation.DateTill <= to)
                    .Select(x => Mapper.Map<ReservationDTO, ReservationPrevDTO>(x.Reservation));
            }
            else
            {
                return result.Select(x => Mapper.Map<ReservationDTO, ReservationPrevDTO>(x.Reservation));
            }
        }
    }
}