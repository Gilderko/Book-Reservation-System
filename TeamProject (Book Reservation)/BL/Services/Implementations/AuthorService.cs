using AutoMapper;
using BL.DTOs.Entities.Author;
using BL.DTOs.Entities.Book;
using BL.DTOs.Filters;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Services.Implementations
{
    class AuthorService : CRUDService<AuthorDTO, Author>, IAuthorService
    {
        public AuthorService(IRepository<Author> repo,
                             IMapper mapper,
                             QueryObject<AuthorDTO, Author> queryObjectBase) : base(repo, mapper, queryObjectBase)
        {

        }

        public async Task LoadAuthors(IEnumerable<BookPrevDTO> previews)
        {
            foreach (var prev in previews)
            {
                foreach (var authorBook in prev.Authors)
                {
                    authorBook.Author = await GetByID(authorBook.AuthorID);
                }
            }
        }

        public async Task<IEnumerable<int>> GetAuthorsBooksIdsByName(string name, string surname)
        {
            List<PredicateDto> predicates = new()
            {
                new PredicateDto(nameof(Author.Name), name, Infrastructure.Query.Operators.ValueComparingOperator.Contains),
                new PredicateDto(nameof(Author.Surname), surname, Infrastructure.Query.Operators.ValueComparingOperator.Contains),
            };

            FilterDto filter = new()
            {
                Predicate = new CompositePredicateDto(predicates, Infrastructure.Query.Operators.LogicalOperator.AND)
            };

            string[] collectionsToLoad = new string[] {
                nameof(Author.AuthorsBooks)
            };

            HashSet<int> bookIds = new();

            var authors = await FilterBy(filter, null, collectionsToLoad);

            foreach (var author in authors)
            {
                foreach (var id in author.AuthorsBooks.Select(a => a.BookID))
                {
                    bookIds.Add(id);
                }
            }

            return bookIds;
        }
    }
}
