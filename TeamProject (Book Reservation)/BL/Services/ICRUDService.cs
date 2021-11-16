using BL.DTOs;
using DAL.Entities;
using System;

namespace BL.Services
{
    public interface ICRUDService<TEntityDTO, TEntity> where TEntity : class, IEntity
                                                  where TEntityDTO : class, IEntityDTO
    {
        public TEntityDTO GetByID(int id, string[] refsToLoad = null, string[] collectToLoad = null);

        public TEntityDTO GetById(int id, Func<TEntity, string[]> refsToLoadFunc = null,
            Func<TEntity, string[]> collectionsToLoadFunc = null);

        public void Insert(TEntityDTO DTOToAdd);

        public void Delete(int id);

        public void Delete(TEntityDTO DTOToDelete);

        public void Update(TEntityDTO DTOToUpdate);
    }
}
