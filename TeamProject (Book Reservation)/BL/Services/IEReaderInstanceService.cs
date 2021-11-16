using BL.DTOs.Entities.EBook;
using BL.DTOs.Entities.EReaderInstance;
using DAL.Entities;

namespace BL.Services
{
    public interface IEReaderInstanceService : ICRUDService<EReaderInstanceDTO, EReaderInstance>
    {
        public void AddEBook(EReaderInstanceDTO eReaderInstance, EBookDTO eBook);

        public void DeleteEBook(EReaderInstanceDTO eReaderInstance, EBookDTO eBook);
    }
}