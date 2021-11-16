using DAL.Entities;
using System.Collections.Generic;
using BL.DTOs.Entities.BookCollection;
using BL.DTOs.Entities.User;

namespace BL.Services
{
    public interface IBookCollectionPreviewService<TEntityDTO, TEntity> : ICRUDService<TEntityDTO, TEntity> where TEntityDTO : BookCollectionPrevDTO
                                                                                                      where TEntity : BookCollection
    {
        public IEnumerable<BookCollectionPrevDTO> GetBookCollectionsByUser(UserDTO user, int pageNumber = 1,
            int pageSize = 20);

    }
}
