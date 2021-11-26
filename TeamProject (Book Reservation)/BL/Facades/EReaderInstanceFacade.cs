using System;
using System.Collections.Generic;
using BL.DTOs.Entities.EBook;
using BL.DTOs.Entities.EReaderInstance;
using BL.DTOs.Entities.Reservation;
using BL.DTOs.ConnectionTables;
using BL.Services;
using DAL.Entities;
using Infrastructure;
using DAL.Entities.ConnectionTables;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class EReaderInstanceFacade
    {
        private IUnitOfWork _unitOfWork;
        private ICRUDService<EReaderInstanceDTO, EReaderInstance> _eReaderInstanceService;
        private ICRUDService<EBookEReaderInstanceDTO, EBookEReaderInstance> _eBookEReaderInstanceService;
        private IReservationService _reservationService;

        public EReaderInstanceFacade(IUnitOfWork unitOfWork,
                                     ICRUDService<EReaderInstanceDTO, EReaderInstance> eReaderInstanceService,
                                     ICRUDService<EBookEReaderInstanceDTO, EBookEReaderInstance> eBookEReaderInstanceService,
                                     IReservationService reservationService)
        {
            _unitOfWork = unitOfWork;
            _eReaderInstanceService = eReaderInstanceService;
            _eBookEReaderInstanceService = eBookEReaderInstanceService;
            _reservationService = reservationService;
        }

        public async Task Create(EReaderInstanceDTO eReaderInstance)
        {
            await _eReaderInstanceService.Insert(eReaderInstance);
            _unitOfWork.Commit();
        }

        public async Task<EReaderInstanceDTO> Get(int id, string[] refsToLoad = null, string[] collectToLoad = null)
        {
            return await _eReaderInstanceService.GetByID(id, refsToLoad, collectToLoad);
        }

        public void Update(EReaderInstanceDTO eReaderInstance)
        {
            _eReaderInstanceService.Update(eReaderInstance);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _eReaderInstanceService.DeleteById(id);
            _unitOfWork.Commit();
        }
        
        public async Task AddEBook(EReaderInstanceDTO eReaderInstance, EBookDTO eBook)
        {
            await _eBookEReaderInstanceService.Insert(new EBookEReaderInstanceDTO
            {
                EReaderInstance = eReaderInstance,
                EReaderInstanceID = eReaderInstance.Id,

                EBook = eBook,
                EBookID = eBook.Id
            });
            _unitOfWork.Commit();
        }
        
        public void DeleteEBook(EReaderInstanceDTO eReaderInstance, EBookDTO eBook)
        {
            _eBookEReaderInstanceService.Delete(new EBookEReaderInstanceDTO
            {
                EReaderInstance = eReaderInstance,
                EReaderInstanceID = eReaderInstance.Id,

                EBook = eBook,
                EBookID = eBook.Id
            });
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<ReservationPrevDTO>> GetReservationPrevsByDate(EReaderInstanceDTO eReader, DateTime from, DateTime to)
        {
            return await _reservationService.GetReservationPrevsByEReader(eReader.Id, from, to);
        }
        
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
