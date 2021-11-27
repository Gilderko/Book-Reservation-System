using System;
using AutoMapper;
using BL.DTOs.Filters;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query.Operators;
using System.Collections.Generic;
using System.Linq;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.User;
using DAL.Entities.ConnectionTables;
using System.Threading.Tasks;

namespace BL.Services.Implementations
{
    public class BookInstancePreviewService : CRUDService<BookInstancePrevDTO, BookInstance>, IBookInstancePreviewService
    {                                                                                                
        private readonly QueryObject<ReservationBookInstanceDTO, ReservationBookInstance> _reservationQueryObject;
        
        public BookInstancePreviewService(IRepository<BookInstance> repo, 
                                          IMapper mapper, 
                                          QueryObject<BookInstancePrevDTO, BookInstance> queryObject,
                                          QueryObject<ReservationBookInstanceDTO, ReservationBookInstance> reservationQueryObject) : base(repo, mapper, queryObject)
        {
            _reservationQueryObject = reservationQueryObject;
        }

        public async Task<IEnumerable<BookInstancePrevDTO>> GetBookInstancePrevsByUser(int userId)
        {
            string[] referencesToLoad = new[]
            {
                nameof(BookInstance.FromBookTemplate)
            };

            FilterDto filter = new FilterDto()
            {
                Predicate = new PredicateDto(nameof(BookInstance.BookOwnerId), userId, ValueComparingOperator.Equal),
            };

            return (await FilterBy(filter,referencesToLoad));
        }

        public async Task<IEnumerable<BookInstancePrevDTO>> GetAvailableInstancePrevsByDate(BookDTO book, DateTime from, DateTime to, int pageNumber = 1, int pageSize = 20)
        { 
            FilterDto filter = new FilterDto()
            {
                Predicate = new PredicateDto(nameof(BookInstanceDTO.BookTemplateID), book.Id, ValueComparingOperator.Equal),
                RequestedPageNumber = pageNumber,
                PageSize = pageSize
            };
            
            var allInstances = (await FilterBy(filter));
            
            await FilterAvailableInstances(allInstances.ToHashSet(), from, to);

            return allInstances;
        }
        
        private async Task FilterAvailableInstances(HashSet<BookInstancePrevDTO> bookInstances, DateTime from, DateTime to)
        {
            string[] referencesToLoad = new[]
            {
                nameof(ReservationBookInstance.Reservation),
                nameof(ReservationBookInstance.BookInstance)
            };
            
            _reservationQueryObject.LoadExplicitReferences(x => referencesToLoad);
            
            FilterDto filter = new FilterDto()
            {
                Predicate = new PredicateDto(nameof(ReservationBookInstance.BookInstanceID), 
                    bookInstances.Select(x => x.Id).ToArray(), ValueComparingOperator.In)
            };

            var reservations = (await _reservationQueryObject.ExecuteQuery(filter)).Items;

            foreach (var reservation in reservations)
            {
                BookInstancePrevDTO instancePrev = Mapper.Map<BookInstancePrevDTO>(reservation.BookInstance);
                
                // instance not removed yet and intervals overlap
                if (bookInstances.Contains(instancePrev) && !(reservation.Reservation.DateFrom > to && from > reservation.Reservation.DateTill))
                {
                    bookInstances.RemoveWhere(x => x.Id == reservation.BookInstanceID);
                }
            }
        }
    }
}
