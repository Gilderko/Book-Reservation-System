using AutoMapper;
using BL.DTOs.Entities.Genre;
using BL.DTOs.Enums;
using BL.DTOs.Filters;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Implementations
{
    public class GenreService : CRUDService<GenreDTO, Genre>, IGenreService
    {
        public GenreService(IRepository<Genre> repo,
                            IMapper mapper,
                            QueryObject<GenreDTO, Genre> resQueryObject) : base(repo, mapper, resQueryObject)
        {
        }


        public async Task<IEnumerable<int>> GetBookIdsByGenres(IEnumerable<GenreTypeDTO> genreIds)
        {
            FilterDto filter = new FilterDto()
            {
                Predicate = new PredicateDto(nameof(Genre.Id), genreIds, Infrastructure.Query.Operators.ValueComparingOperator.In)
            };

            string[] collectionsToLoad = new string[] {
                nameof(Genre.Books)
            };

            var genres = await FilterBy(filter, null, collectionsToLoad);

            HashSet<int> bookIds = new();

            foreach (var genre in genres.items)
            {
                foreach (var id in genre.Books.Select(b => b.BookID))
                {
                    bookIds.Add(id);
                }
            }

            return bookIds;
        }
    }
}
