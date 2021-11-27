using BL.DTOs.Entities.Author;
using BL.DTOs.Entities.Book;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IAuthorService : ICRUDService<AuthorDTO, Author>
    {
        public Task LoadAuthors(IEnumerable<BookPrevDTO> previews);

        public Task<IEnumerable<int>> GetAuthorsBooksIdsByName(string name, string surname);
    }
}
