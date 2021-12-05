﻿using System;
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
using BL.DTOs.Filters;
using BL.DTOs;
using System.Linq;
using BL.DTOs.Entities.EReader;
using Infrastructure.Query.Operators;

namespace BL.Facades
{
    public class EReaderInstanceFacade
    {
        private IUnitOfWork _unitOfWork;
        private IEReaderInstanceService _eReaderInstanceService;
        private ICRUDService<EBookEReaderInstanceDTO, EBookEReaderInstance> _eBookEReaderInstanceService;
        private ICRUDService<EReaderPrevDTO, EReader> _eReaderPrevService;
        private ICRUDService<EReaderInstancePrevDTO, EReaderInstance> _eReaderInstancePrevDTO;
        private IReservationService _reservationService;
        private IEReaderInstancePreviewService _eReaderInstancePrevService;

        public EReaderInstanceFacade(IUnitOfWork unitOfWork,
                                     IEReaderInstanceService eReaderInstanceService,
                                     ICRUDService<EBookEReaderInstanceDTO, EBookEReaderInstance> eBookEReaderInstanceService,
                                     IReservationService reservationService,
                                     IEReaderInstancePreviewService eReaderInstancePrevService,
                                     ICRUDService<EReaderPrevDTO, EReader> eReaderPrevService,
                                     ICRUDService<EReaderInstancePrevDTO, EReaderInstance> eReaderInstancePrevDTO)
        {
            _unitOfWork = unitOfWork;
            _eReaderInstanceService = eReaderInstanceService;
            _eBookEReaderInstanceService = eBookEReaderInstanceService;
            _reservationService = reservationService;
            _eReaderInstancePrevService = eReaderInstancePrevService;
            _eReaderPrevService = eReaderPrevService;
            _eReaderInstancePrevDTO = eReaderInstancePrevDTO;
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

        public async Task AddNewEReaderToUser(EReaderInstanceCreateDTO eReader, int userId)
        {
            await _eReaderInstanceService.AddEReaderInstanceToUser(eReader, userId);
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<EReaderInstancePrevDTO>> GetEReaderInstancesByOwner(int ownerId)
        {
            return await _eReaderInstancePrevService.GetEReaderInstancesByOwner(ownerId);
        }

        public async Task<IEnumerable<EReaderInstancePrevDTO>> GetEReaderInstancePrevsBy(string description,
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
                List<int> eReaders = (await _eReaderPrevService.FilterBy(eReaderFilter)).Select(x => x.Id).ToList();

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

            return await _eReaderInstancePrevDTO.FilterBy(instancesFilter, refsToLoad, null);
        }

        public async Task<IEnumerable<EReaderPrevDTO>> GetEReaderTemplates()
        {
            FilterDto filter = new()
            {
                SortAscending = true,
                SortCriteria = nameof(EReader.CompanyMake)
            };

            return await _eReaderPrevService.FilterBy(filter);
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
            return result;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
