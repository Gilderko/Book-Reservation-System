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
        private IRepository<TEntity> repository;
        private IMapper mapper;

        public CRUDService(IUnitOfWork unitOfWork)
        {
            repository = new Repository<TEntity>(unitOfWork);
            mapper = new Mapper(new MapperConfiguration(MappingProfile.ConfigureMapping));
        }

        public TEntityDTO GetByID(int id, string[] refsToLoad = null, string[] collectToLoad = null)
        {
            TEntity resultEntity = repository.GetByID(id, refsToLoad, collectToLoad);
            TEntityDTO resultDTO = mapper.Map<TEntityDTO>(resultEntity);
            return resultDTO;
        }

        public TEntityDTO GetById(int id, Func<TEntity, string[]> refsToLoadFunc = null, Func<TEntity, string[]> collectionsToLoadFunc = null)
        {
            return GetByID(id, refsToLoadFunc == null ? new string[0] : refsToLoadFunc.Invoke(null),
                collectionsToLoadFunc == null ? new string[0] : collectionsToLoadFunc.Invoke(null));
        }

        public void Insert(TEntityDTO DTOToAdd)
        {
            TEntity entityToAdd = mapper.Map<TEntity>(DTOToAdd);
            repository.Insert(entityToAdd);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public void Delete(TEntityDTO DTOToDelete)
        {
            TEntity entityToDelete = mapper.Map<TEntity>(DTOToDelete);
            repository.Delete(entityToDelete);
        }

        public void Update(TEntityDTO DTOToUpdate)
        {
            TEntity entityToUpdate = mapper.Map<TEntity>(DTOToUpdate);
            repository.Update(entityToUpdate);
        }
    }
}
