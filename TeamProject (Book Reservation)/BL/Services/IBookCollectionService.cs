using BL.DTOs.Entities.BookCollection;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IBookCollectionService : ICRUDService<BookCollectionDTO, BookCollection>
    {
        public Task<IEnumerable<BookCollectionPrevDTO>> GetBookCollectionPrevsByUser(int userId);

        public Task CreateUserCollection(BookCollectionCreateDTO bookCollection, int userId);

        public Task<BookCollectionCreateDTO> GetUserCollectionToEdit(int id);

        public void EditUserCollection(BookCollectionCreateDTO collection);
    }
}
