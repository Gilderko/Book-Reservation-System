using System.Collections.Generic;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.EReaderInstance;
using BL.DTOs.Entities.Reservation;
using BL.Services;
using DAL.Entities;
using Infrastructure;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class ReservationFacade
    {
        private IUnitOfWork _unitOfWork;
        private IReservationService _service;
        private ICRUDService<BookInstanceDTO, BookInstance> _bookInstService;
        private ICRUDService<EReaderInstanceDTO, EReaderInstance> _EReaderInstanceService;

        public ReservationFacade(IUnitOfWork unitOfWork, 
                                 IReservationService service,
                                 ICRUDService<BookInstanceDTO, BookInstance> bookInstanceService,
                                 ICRUDService<EReaderInstanceDTO, EReaderInstance> eReaderInstanceService)
        {
            _unitOfWork = unitOfWork;
            _service = service;
            _bookInstService = bookInstanceService;
            _EReaderInstanceService = eReaderInstanceService;
        }

        public async Task Create(ReservationDTO reservation)
        {
            await _service.Insert(reservation);
        }

        public async Task<ReservationDTO> Get(int id, string[] refsToLoad = null, string[] collectToLoad = null)
        {
            return await _service.GetByID(id, refsToLoad, collectToLoad);
        }

        public void Update(ReservationDTO reservation)
        {
            _service.Update(reservation);
        }

        public void Delete(int id)
        {
            _service.DeleteById(id);
        }

        public async Task AddBookInstance(int bookInstanceId, ReservationDTO reservation)
        {
            var referencesToLoad = new string[]
            {
                nameof(BookInstanceDTO.FromBookTemplate),
                nameof(BookInstanceDTO.Owner)
            };

            var reservations = await _service.GetReservationPrevsByBookInstance(bookInstanceId, reservation.DateFrom, reservation.DateTill);

            if (!CheckIsAvailable(reservations,reservation))
            {
                return;
            }

            var bookInstance =  await _bookInstService.GetByID(bookInstanceId, referencesToLoad);

            if (bookInstance == null)
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

            var reservations = await _service.GetReservationPrevsByEReader(eReaderInstanceId, reservation.DateFrom, reservation.DateTill);

            if (!CheckIsAvailable(reservations, reservation))
            {
                return;
            }

            var eReaderInstance = await _EReaderInstanceService.GetByID(eReaderInstanceId, referencesToLoad);

            if (eReaderInstance == null)
            {
                return;
            }

            reservation.EReaderID = eReaderInstanceId;
            reservation.EReader = eReaderInstance;     

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
                var booksReservations = await _service.GetReservationPrevsByBookInstance(bookInst.BookInstanceID,
                    newReservation.DateFrom, newReservation.DateTill);

                if (!CheckIsAvailable(booksReservations, newReservation))
                {
                    return false;
                }
            }

            var EReader = newReservation.EReader;

            if (EReader != null)
            {
                var EReaderReservations = await _service.GetReservationPrevsByEReader(EReader.Id, newReservation.DateFrom, newReservation.DateTill);

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

            await _service.Insert(newReservation);

            _unitOfWork.Commit();
            return true;
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
