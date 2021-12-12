using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.EReader;
using BL.DTOs.Entities.EReaderInstance;
using BL.DTOs.Entities.Reservation;
using BL.DTOs.Filters;
using BL.Services;
using DAL.Entities;
using DAL.Entities.ConnectionTables;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Query.Operators;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class EReaderInstanceFacade
    {
        private IUnitOfWork _unitOfWork;
        private IEReaderInstanceService _eReaderInstanceService;
        private ICRUDService<AddEBookInEReaderInstanceDTO, EBookEReaderInstance> _addeBookEReaderInstanceService;
        private ICRUDService<EBookEReaderInstanceDTO, EBookEReaderInstance> _eBookEReaderInstanceService;
        private ICRUDService<EReaderPrevDTO, EReader> _eReaderPrevService;
        private IReservationService _reservationService;
        private IEReaderInstancePreviewService _eReaderInstancePrevService;

        public EReaderInstanceFacade(IUnitOfWork unitOfWork,
                                     IEReaderInstanceService eReaderInstanceService,
                                     ICRUDService<AddEBookInEReaderInstanceDTO, EBookEReaderInstance> addeBookEReaderInstanceService,
                                     IReservationService reservationService,
                                     IEReaderInstancePreviewService eReaderInstancePrevService,
                                     ICRUDService<EReaderPrevDTO, EReader> eReaderPrevService,
                                     ICRUDService<EBookEReaderInstanceDTO, EBookEReaderInstance> eBookEReaderInstanceService,
                                     ICRUDService<EReaderInstancePrevDTO, EReaderInstance> eReaderInstancePrevDTO)
        {
            _unitOfWork = unitOfWork;
            _eReaderInstanceService = eReaderInstanceService;
            _addeBookEReaderInstanceService = addeBookEReaderInstanceService;
            _reservationService = reservationService;
            _eReaderInstancePrevService = eReaderInstancePrevService;
            _eReaderPrevService = eReaderPrevService;
            _eBookEReaderInstanceService = eBookEReaderInstanceService;
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

        public async Task AddEBook(AddEBookInEReaderInstanceDTO eBookEReader)
        {
            await _addeBookEReaderInstanceService.Insert(eBookEReader);
            _unitOfWork.Commit();
        }

        public void DeleteEBook(int eReaderId, int eBookId)
        {
            _addeBookEReaderInstanceService.Delete(new AddEBookInEReaderInstanceDTO
            {
                EReaderInstanceID = eReaderId,
                EBookID = eBookId
            });
            _unitOfWork.Commit();
        }

        public async Task<bool> CheckIfAlreadyHasBook(int ereaderId,int bookdId)
        {
            var collsToLoad = new string[]
            {
                nameof(EReaderInstance.BooksIncluded)
            };

            var eReader = await _eReaderInstanceService.GetByID(ereaderId, null, collsToLoad);
            return eReader.BooksIncluded.Any(entry => entry.EBookID == bookdId);
        }

        public async Task AddNewEReaderToUser(EReaderInstanceCreateDTO eReader, int userId)
        {
            await _eReaderInstanceService.AddEReaderInstanceToUser(eReader, userId);
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<EReaderInstancePrevDTO>> GetEReaderInstancesByOwner(int ownerId)
        {
            return await _eReaderInstancePrevService.GetEReaderInstancesByOwner(ownerId);
        }

        public async Task<(IEnumerable<EReaderInstancePrevDTO>,int)> GetEReaderInstancePrevsBy(int? page,
                                                                                         int? pageSize,
                                                                                         string description,
                                                                                         string company,
                                                                                         string model,
                                                                                         int? memorySizeFrom,
                                                                                         int? memorySizeTo)
        {
            FilterDto eReaderFilter = new FilterDto();
            List<PredicateDto> eReaderPredicates = new List<PredicateDto>();

            FilterDto instancesFilter = new FilterDto();
            List<PredicateDto> instancesPredicates = new List<PredicateDto>();

            if (company is not null)
            {
                eReaderPredicates.Add(new PredicateDto(nameof(EReader.CompanyMake), company, ValueComparingOperator.Contains));
            }

            if (model is not null)
            {
                eReaderPredicates.Add(new PredicateDto(nameof(EReader.Model), model, ValueComparingOperator.Contains));
            }

            if (memorySizeFrom is not null)
            {
                eReaderPredicates.Add(new PredicateDto(nameof(EReader.MemoryInMB), memorySizeFrom, ValueComparingOperator.GreaterThanOrEqual));
            }

            if (memorySizeTo is not null)
            {
                eReaderPredicates.Add(new PredicateDto(nameof(EReader.MemoryInMB), memorySizeTo, ValueComparingOperator.LessThanOrEqual));
            }

            if (eReaderPredicates.Count > 0)
            {
                eReaderFilter.Predicate = new CompositePredicateDto(eReaderPredicates, LogicalOperator.AND);
                List<int> eReaders = (await _eReaderPrevService.FilterBy(eReaderFilter)).items.Select(x => x.Id).ToList();

                instancesPredicates.Add(new PredicateDto(nameof(EReaderInstance.EReaderTemplateID), eReaders, ValueComparingOperator.In));
            }

            if (description is not null)
            {
                instancesPredicates.Add(new PredicateDto(nameof(EReaderInstance.Description), description, ValueComparingOperator.Contains));
            }

            string[] refsToLoad = new string[]
            {
                nameof(EReaderInstance.EReaderTemplate)
            };

            if (instancesPredicates.Count > 0)
            {
                instancesFilter.Predicate = new CompositePredicateDto(instancesPredicates, LogicalOperator.AND);
            }

            var result = await _eReaderInstancePrevService.FilterBy(instancesFilter, refsToLoad, null);

            return result;
        }

        public async Task<IEnumerable<EReaderPrevDTO>> GetEReaderTemplates()
        {
            FilterDto filter = new()
            {
                SortAscending = true,
                SortCriteria = nameof(EReader.CompanyMake)
            };

            var result = await _eReaderPrevService.FilterBy(filter);

            return result.items;
        }

        public async Task<IEnumerable<ReservationPrevDTO>> GetReservationPrevsByDate(EReaderInstanceDTO eReader, DateTime from, DateTime to)
        {
            return await _reservationService.GetReservationPrevsByEReader(eReader.Id, from, to);
        }

        public async Task<IEnumerable<EBookEReaderInstanceDTO>> GetEBookEReaderInstancesByEReaderInstance(int id)
        {
            var filter = new FilterDto
            {
                Predicate = new PredicateDto(nameof(EBookEReaderInstanceDTO.EReaderInstanceID), id, ValueComparingOperator.Equal)
            };

            var refsToLoad = new[]
            {
                nameof(EBookEReaderInstanceDTO.EBook)
            };

            var result = await _eBookEReaderInstanceService.FilterBy(filter, refsToLoad);
            return result.items;
        }
    }
}
