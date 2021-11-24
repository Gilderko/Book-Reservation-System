using DAL.Entities;
using System.Collections.Generic;
using BL.DTOs.Entities.BookCollection;
using BL.DTOs.Entities.User;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IBookCollectionPreviewService : ICRUDService<BookCollectionPrevDTO, BookCollection>
    {
        public Task<IEnumerable<BookCollectionPrevDTO>> GetBookCollectionsByUser(UserDTO user, int pageNumber = 1,
            int pageSize = 20);

    }
}
