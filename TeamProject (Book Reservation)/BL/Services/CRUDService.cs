using AutoMapper;
using BL.Config;
using BL.DTOs;
using DAL.Entities;
using EFInfrastructure;
using Infrastructure;
using System;

namespace BL.Services
{
    public class CRUDService<TEntityDTO, TEntity> where TEntity : class, IEntity
                                                  where TEntityDTO : class, IEntityDTO
    {       
        private IRepository<TEntity> _repository;
        private IMapper _mapper;

        public CRUDService(IRepository<TEntity> repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        public TEntityDTO GetByID(int id, string[] refsToLoad = null, string[] collectToLoad = null)
        {
            TEntity resultEntity = _repository.GetByID(id, refsToLoad, collectToLoad);
            TEntityDTO resultDTO = _mapper.Map<TEntityDTO>(resultEntity);
            return resultDTO;
        }

        public TEntityDTO GetById(int id, Func<TEntity, string[]> refsToLoadFunc = null, Func<TEntity, string[]> collectionsToLoadFunc = null)
        {
            return GetByID(id, refsToLoadFunc == null ? new string[0] : refsToLoadFunc.Invoke(null),
                collectionsToLoadFunc == null ? new string[0] : collectionsToLoadFunc.Invoke(null));
        }

        public void Insert(TEntityDTO DTOToAdd)
        {
            TEntity entityToAdd = _mapper.Map<TEntity>(DTOToAdd);
            _repository.Insert(entityToAdd);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Delete(TEntityDTO DTOToDelete)
        {
            TEntity entityToDelete = _mapper.Map<TEntity>(DTOToDelete);
            _repository.Delete(entityToDelete);
        }

        public void Update(TEntityDTO DTOToUpdate)
        {
            TEntity entityToUpdate = _mapper.Map<TEntity>(DTOToUpdate);
            _repository.Update(entityToUpdate);
        }
    }
}
