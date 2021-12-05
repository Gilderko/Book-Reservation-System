using AutoMapper;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Author;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.EBook;
using BL.DTOs.Filters;
using BL.QueryObjects;
using DAL.Entities;
using DAL.Entities.ConnectionTables;
using Infrastructure;
using Infrastructure.Query.Operators;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Services.Implementations
{
    class AuthorService : CRUDService<AuthorDTO, Author>, IAuthorService
    {
        private readonly QueryObject<AuthorBookDTO, AuthorBook> _queryObjectBookAuthor;


        public AuthorService(IRepository<Author> repo,
                             IMapper mapper,
                             QueryObject<AuthorDTO, Author> queryObjectBase,
                             QueryObject<AuthorBookDTO, AuthorBook> queryObjectBookAuthor) : base(repo, mapper, queryObjectBase)
        {
            _queryObjectBookAuthor = queryObjectBookAuthor;
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

        public async Task LoadAuthors(IEnumerable<EBookPrevDTO> previews)
        {
            foreach (var prev in previews)
            {
                foreach (var authorBook in prev.Authors)
                {
                    authorBook.Author = await GetByID(authorBook.AuthorID);
                }
            }
        }

        public async Task LoadAuthors(IEnumerable<BookInstancePrevDTO> previews)
        {
            foreach (var prev in previews)
            {
                FilterDto filter = new()
                {
                    Predicate = new PredicateDto(nameof(AuthorBook.BookID), prev.FromBookTemplate.Id, ValueComparingOperator.Equal)
                };
                prev.FromBookTemplate.Authors = _queryObjectBookAuthor.ExecuteQuery(filter).Result.Items;

                foreach (var authorBook in prev.FromBookTemplate.Authors)
                {
                    authorBook.Author = await GetByID(authorBook.AuthorID);
                }
            }
        }

        public async Task<IEnumerable<int>> GetAuthorsBooksIdsByName(string name, string surname)
        {
            List<PredicateDto> predicates = new();

            if (name is not null)
            {
                predicates.Add(new PredicateDto(nameof(Author.Name), name, ValueComparingOperator.Contains));
            }

            if (surname is not null)
            {
                predicates.Add(new PredicateDto(nameof(Author.Surname), surname, ValueComparingOperator.Contains));
            }

            FilterDto filter = new()
            {
                Predicate = new CompositePredicateDto(predicates, LogicalOperator.AND)
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
