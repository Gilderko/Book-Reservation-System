using BL.DTOs.Entities.Book;
using DAL.Entities;

namespace BL.Services
{
    public interface IBookPreviewService : ICRUDService<BookPrevDTO, Book>
    {
    }
}
