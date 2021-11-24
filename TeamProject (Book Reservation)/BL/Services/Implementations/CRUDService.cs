using AutoMapper;
using BL.DTOs;
using DAL.Entities;
using Infrastructure;
using System;
using System.Threading.Tasks;

namespace BL.Services.Implementations
{
    public class CRUDService<TEntityDTO, TEntity> : ICRUDService<TEntityDTO, TEntity> where TEntity : class, IEntity
                                                                                      where TEntityDTO : class, IEntityDTO
    {
        private IRepository<TEntity> _repository;
        protected IMapper Mapper { get; private set; }

        public CRUDService(IRepository<TEntity> repo, IMapper mapper)
        {
            _repository = repo;
            Mapper = mapper;
        }

        public async Task<TEntityDTO> GetByID(int id, string[] refsToLoad = null, string[] collectToLoad = null)
        {
            TEntity resultEntity = await _repository.GetByID(id, refsToLoad, collectToLoad);
            TEntityDTO resultDTO = Mapper.Map<TEntityDTO>(resultEntity);
            return resultDTO;
        }

        public async Task<TEntityDTO> GetById(int id, Func<TEntity, string[]> refsToLoadFunc = null, Func<TEntity, string[]> collectionsToLoadFunc = null)
        {
            return await GetByID(id, refsToLoadFunc == null ? new string[0] : refsToLoadFunc.Invoke(null),
                collectionsToLoadFunc == null ? new string[0] : collectionsToLoadFunc.Invoke(null));
        }

        public async Task Insert(TEntityDTO DTOToAdd)
        {
            TEntity entityToAdd = Mapper.Map<TEntity>(DTOToAdd);
            await _repository.Insert(entityToAdd);
        }

        public void DeleteById(int id)
        {
            _repository.DeleteById(id);
        }

        public void Delete(TEntityDTO DTOToDelete)
        {
            TEntity entityToDelete = Mapper.Map<TEntity>(DTOToDelete);
            _repository.Delete(entityToDelete);
        }

        public void Update(TEntityDTO DTOToUpdate)
        {
            TEntity entityToUpdate = Mapper.Map<TEntity>(DTOToUpdate);
            _repository.Update(entityToUpdate);
        }
    }
}
