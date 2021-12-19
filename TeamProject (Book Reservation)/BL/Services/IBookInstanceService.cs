using BL.DTOs.Entities.BookInstance;
using DAL.Entities;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IBookInstanceService : ICRUDService<BookInstanceDTO, BookInstance>
    {
        public Task CreateBookInstance(int ownerId, int bookTemplateId, BookInstanceCreateDTO createBookInstance);
    }
}
