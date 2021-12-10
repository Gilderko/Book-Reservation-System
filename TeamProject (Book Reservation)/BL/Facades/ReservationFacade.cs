using System.Collections.Generic;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.EReaderInstance;
using BL.DTOs.Entities.Reservation;
using BL.Services;
using DAL.Entities;
using Infrastructure;
using System.Threading.Tasks;
using BL.DTOs.Filters;
using Infrastructure.Query.Operators;
using System.Linq;
using DAL.Entities.ConnectionTables;

namespace BL.Facades
{
    public class ReservationFacade
    {
        private IUnitOfWork _unitOfWork;
        private IReservationService _reservationService;
        private ICRUDService<BookInstanceDTO, BookInstance> _bookInstService;
        private ICRUDService<EReaderInstanceDTO, EReaderInstance> _EReaderInstanceService;
        private ICRUDService<ReservationBookInstanceDTO, ReservationBookInstance> _reservationBookInstanceService;

        public ReservationFacade(IUnitOfWork unitOfWork, 
                                 IReservationService service,
                                 ICRUDService<BookInstanceDTO, BookInstance> bookInstanceService,
                                 ICRUDService<EReaderInstanceDTO, EReaderInstance> eReaderInstanceService,
                                 ICRUDService<ReservationBookInstanceDTO, ReservationBookInstance> reservationBookInstanceService)
        {
            _unitOfWork = unitOfWork;
            _reservationService = service;
            _bookInstService = bookInstanceService;
            _EReaderInstanceService = eReaderInstanceService;
            _reservationBookInstanceService = reservationBookInstanceService;
        }

        public async Task<IEnumerable<ReservationDTO>> Index()
        {
            FilterDto filter = new FilterDto()
            {
                Predicate = null,
                SortCriteria = nameof(Reservation.UserID),
                SortAscending = true
            };

            var result = await _reservationService.FilterBy(filter);

            return result.items;
        }

        public async Task Create(ReservationDTO reservation)
        {
            await _reservationService.Insert(reservation);
            _unitOfWork.Commit();
        }

        public async Task<ReservationDTO> Get(int id, string[] refsToLoad = null, string[] collectToLoad = null)
        {
            return await _reservationService.GetByID(id, refsToLoad, collectToLoad);
        }

        public async Task<ReservationDTO> GetDetailWithLoadedBooks(int id)
        {
            var collRefToLoad = new string[]
            {
                nameof(ReservationDTO.BookInstances)
            };

            // Got the reservation
            var reservation = await _reservationService.GetByID(id, null, collRefToLoad);            

            var refBookInstToLoad = new string[]
            {
                nameof(BookInstanceDTO.FromBookTemplate),
                nameof(BookInstanceDTO.Owner)
            };

            var refEReaderInstToLoad = new string[]
            {
                nameof(EReaderInstanceDTO.Owner)
            };

            if (reservation.EReaderID != null)
            {
                reservation.EReader = await _EReaderInstanceService.GetByID(reservation.EReaderID.Value, refEReaderInstToLoad);
            }

            foreach (var reserv in reservation.BookInstances)
            {
                reserv.BookInstance = await _bookInstService.GetByID(reserv.BookInstanceID, refBookInstToLoad);
            }

            return reservation;
        }

        public void Update(ReservationDTO reservation)
        {
            _reservationService.Update(reservation);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _reservationService.DeleteById(id);
            _unitOfWork.Commit();
        }

        public async Task AddBookInstance(int bookInstanceId, ReservationDTO reservation)
        {
            if (reservation.BookInstances.Any(entry => entry.BookInstanceID == bookInstanceId))
            {
                return;
            }

            var referencesToLoad = new string[]
            {
                nameof(BookInstanceDTO.FromBookTemplate),
                nameof(BookInstanceDTO.Owner)
            };

            var bookInstance = await _bookInstService.GetByID(bookInstanceId, referencesToLoad);

            if (bookInstance == null)
            {
                return;
            }

            var reservations = await _reservationService.GetReservationPrevsByBookInstance(bookInstanceId, null, null);

            if (!CheckIsAvailable(reservations,reservation))
            {
                return;
            }            

            ReservationBookInstanceDTO resBookInstance = new ReservationBookInstanceDTO()
            {
                BookInstance = bookInstance,
                BookInstanceID = bookInstanceId,

                ReservationID = reservation.Id,
            };

            reservation.BookInstances.Add(resBookInstance);

            _unitOfWork.Commit();
        }

        public async Task AddEReaderInstance(int eReaderInstanceId, ReservationDTO reservation)
        {
            var referencesToLoad = new string[]
            {
                nameof(EReaderInstanceDTO.Owner),               
            };

            var eReaderInstance = await _EReaderInstanceService.GetByID(eReaderInstanceId, referencesToLoad);

            if (eReaderInstance == null)
            {
                return;
            }

            var reservations = await _reservationService.GetReservationPrevsByEReader(eReaderInstanceId, null, null);

            if (!CheckIsAvailable(reservations, reservation))
            {
                return;
            }            

            reservation.EReaderID = eReaderInstanceId;
            reservation.EReader = eReaderInstance;     

            _unitOfWork.Commit();
        }

        public void RemoveBookInstances(ReservationDTO reservation, int[] bookInstancesToRemove)
        {
            foreach (int bookInstId in bookInstancesToRemove)
            {
                var resBookInstance = new ReservationBookInstanceDTO()
                {
                    ReservationID = reservation.Id,
                    BookInstanceID = bookInstId
                };

                _reservationBookInstanceService.Delete(resBookInstance);
            }

            _unitOfWork.Commit();
        }

        public void RemoveEReaderInstance(ReservationDTO reservation)
        {
            reservation.EReaderID = null;

            _reservationService.Update(reservation);

            _unitOfWork.Commit();
        }

        public async Task<bool> SubmitReservation(ReservationDTO newReservation)
        {
            if (newReservation.DateTill < newReservation.DateFrom)
            {
                return false;
            }

            foreach (var bookInst in newReservation.BookInstances)
            {
                var booksReservations = await _reservationService.GetReservationPrevsByBookInstance(bookInst.BookInstanceID,
                    newReservation.DateFrom, newReservation.DateTill);

                if (!CheckIsAvailable(booksReservations, newReservation))
                {
                    return false;
                }
            }

            var EReader = newReservation.EReader;

            if (EReader != null)
            {
                var EReaderReservations = await _reservationService.GetReservationPrevsByEReader(EReader.Id, newReservation.DateFrom, newReservation.DateTill);

                if (!CheckIsAvailable(EReaderReservations, newReservation))
                {
                    return false;
                }
            }

            newReservation.User = null;
            newReservation.EReader = null;
            
            foreach(var bookInstAndReserv in newReservation.BookInstances)
            {
                bookInstAndReserv.BookInstance = null;
                bookInstAndReserv.Reservation = null;
            }

            await _reservationService.Insert(newReservation);

            _unitOfWork.Commit();
            return true;
        }

        public async Task<IEnumerable<ReservationDTO>> GetReservationsByUserId(int userId)
        {
            var predicate = new PredicateDto(nameof(Reservation.UserID), userId, ValueComparingOperator.Equal);

            var collToLoad = new string[]
            {
                nameof(ReservationDTO.BookInstances)
            };

            var refsToLoad = new string[]
            {
                nameof(ReservationDTO.EReader)
            };

            FilterDto filter = new FilterDto()
            {
                Predicate = predicate,
                SortCriteria = nameof(Reservation.DateFrom),
                SortAscending = false
            };

            var result = await _reservationService.FilterBy(filter, refsToLoad, collToLoad);

            return result.items;
        }

        private bool CheckIsAvailable(IEnumerable<ReservationPrevDTO> reservations, ReservationDTO newReservation)
        {
            foreach (var reservation in reservations)
            {
                if (newReservation.DateFrom < reservation.DateTill)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
