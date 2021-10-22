using AutoMapper;
using BL.Config;
using BL.DTOs;
using DAL.Entities;
using EFInfrastructure;
using Infrastructure;

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

        public TEntityDTO GetByID(int id)
        {
            TEntity resultEntity = repository.GetByID(id);
            TEntityDTO resultDTO = mapper.Map<TEntityDTO>(resultEntity);
            return resultDTO;
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
