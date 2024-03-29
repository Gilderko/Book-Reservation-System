﻿using BL.DTOs.Entities.EReader;
using BL.DTOs.Filters;
using BL.Services;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query.Operators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class EReaderFacade
    {
        private IUnitOfWork _unitOfWork;
        private ICRUDService<EReaderDTO, EReader> _service;

        public EReaderFacade(IUnitOfWork unitOfWork, ICRUDService<EReaderDTO, EReader> service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public async Task<(IEnumerable<EReaderDTO>, int)> GetAllEReaders()
        {
            var simplePredicate = new PredicateDto(nameof(EReaderDTO.Id), 1, ValueComparingOperator.GreaterThanOrEqual);

            var filter = new FilterDto()
            {
                Predicate = simplePredicate
            };

            return await _service.FilterBy(filter);
        }

        public async Task Create(EReaderDTO eReader)
        {
            await _service.Insert(eReader);
            _unitOfWork.Commit();
        }

        public async Task<EReaderDTO> Get(int id, string[] refsToLoad = null, string[] collectToLoad = null)
        {
            return await _service.GetByID(id, refsToLoad, collectToLoad);
        }

        public void Update(EReaderDTO eReader)
        {
            _service.Update(eReader);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _service.DeleteById(id);
            _unitOfWork.Commit();
        }
    }
}
