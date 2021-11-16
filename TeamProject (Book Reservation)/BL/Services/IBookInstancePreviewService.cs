using DAL.Entities;
using System.Collections.Generic;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.User;

namespace BL.Services
{
    public interface IBookInstancePreviewService<TEntityDTO, TEntity> : ICRUDService<TEntityDTO, TEntity> where TEntity : BookInstance
                                                                                                    where TEntityDTO : BookInstanceDTO
    {

        public IEnumerable<BookInstancePrevDTO> GetBookInstancesByUser(UserDTO user, int pageNumber = 1,
            int pageSize = 20);
    }
}
