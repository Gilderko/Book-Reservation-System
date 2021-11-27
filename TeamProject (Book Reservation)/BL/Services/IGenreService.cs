using BL.DTOs.Entities.Genre;
using BL.DTOs.Enums;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IGenreService : ICRUDService<GenreDTO, Genre>
    {
        public Task<IEnumerable<int>> GetBookIdsByGenres(IEnumerable<GenreTypeDTO> genres);
    }
}
