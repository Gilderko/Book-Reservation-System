using BL.DTOs.Entities.Author;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.EBook;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IAuthorService : ICRUDService<AuthorDTO, Author>
    {
        public Task LoadAuthors(IEnumerable<BookPrevDTO> previews);

        public Task<IEnumerable<int>> GetAuthorsBooksIdsByName(string name, string surname);

        public Task LoadAuthors(IEnumerable<BookInstancePrevDTO> previews);

        public Task LoadAuthors(IEnumerable<EBookPrevDTO> previews);
    }
}
