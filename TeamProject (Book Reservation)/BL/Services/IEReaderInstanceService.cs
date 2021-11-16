using BL.DTOs.Entities.EBook;
using BL.DTOs.Entities.EReaderInstance;
using DAL.Entities;

namespace BL.Services
{
    public interface IEReaderInstanceService<TEntityDTO, TEntity> : ICRUDService<TEntityDTO, TEntity> where TEntity : EReaderInstance
                                                                                                where TEntityDTO : EReaderInstanceDTO
    {
        public void AddEBook(EReaderInstanceDTO eReaderInstance, EBookDTO eBook);

        public void DeleteEBook(EReaderInstanceDTO eReaderInstance, EBookDTO eBook);
    }
}