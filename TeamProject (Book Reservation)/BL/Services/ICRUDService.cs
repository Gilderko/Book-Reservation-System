using BL.DTOs;
using BL.DTOs.Filters;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface ICRUDService<TEntityDTO, TEntity> where TEntity : class, IEntity
                                                  where TEntityDTO : class, IEntityDTO
    {
        public Task<TEntityDTO> GetByID(int id, string[] refsToLoad = null, string[] collectToLoad = null);

        public Task<TEntityDTO> GetById(int id, Func<TEntity, string[]> refsToLoadFunc = null,
            Func<TEntity, string[]> collectionsToLoadFunc = null);

        public Task Insert(TEntityDTO DTOToAdd);

        public void DeleteById(int id);

        public void Delete(TEntityDTO DTOToDelete);

        public void Update(TEntityDTO DTOToUpdate);

        Task<(IEnumerable<TEntityDTO> items, int totalItemsCount)> FilterBy(FilterDto filter, string[] refsToLoad = null, string[] collectToLoad = null);
    }
}
