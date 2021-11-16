using BL.DTOs.Entities.Book;
using DAL.Entities;

namespace BL.Services
{
    public interface IBookPreviewService<TEntityDTO, TEntity> : ICRUDService<TEntityDTO, TEntity> where TEntity : Book
                                                                                                  where TEntityDTO : BookPrevDTO
    {
    }
}
