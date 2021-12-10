using Autofac.Features.OwnedInstances;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Author;
using BL.DTOs.Entities.Book;
using BL.DTOs.Filters;
using BL.Services;
using DAL.Entities;
using DAL.Entities.ConnectionTables;
using Infrastructure;
using Infrastructure.Query.Operators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class AuthorFacade
    {
        private IUnitOfWork _unitOfWork;
        private ICRUDService<AuthorDTO, Author> _authorService;
        private ICRUDService<AuthorBookDTO, AuthorBook> _authorBookService;
        private ICRUDService<AuthorPrevDTO, Author> _authorPrevService;

        public AuthorFacade(IUnitOfWork unitOfWork,
                            ICRUDService<AuthorDTO, Author> authorService,
                            ICRUDService<AuthorBookDTO, AuthorBook> authorBookService,
                            ICRUDService<AuthorPrevDTO, Author> authorPrevService) 
        {
            _unitOfWork = unitOfWork;
            _authorService = authorService;
            _authorBookService = authorBookService;
            _authorPrevService = authorPrevService;
        }

        public async Task Create(AuthorDTO author)
        {
            await _authorService.Insert(author);
            _unitOfWork.Commit();
        }

        public async Task<AuthorDTO> Get(int id, string[] refsToLoad = null, string[] collectToLoad = null)
        {
            var result = await _authorService.GetByID(id, refsToLoad, collectToLoad);
            return result;
        }

        public void Update(AuthorDTO author)
        {
            _authorService.Update(author);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _authorService.DeleteById(id);
            _unitOfWork.Commit();
        }

        public async Task AddBookToAuthor(AuthorDTO author, BookDTO book)
        {
            await _authorBookService.Insert(new AuthorBookDTO
            {                
                Author = author,
                AuthorID = author.Id,


                Book = book,
                BookID = book.Id
            });
            _unitOfWork.Commit();
        }

        public void DeleteBookFromAuthor(AuthorDTO author, BookDTO book)
        {
            _authorBookService.Delete(new AuthorBookDTO
            {
                Author = author,
                AuthorID = author.Id,


                Book = book,
                BookID = book.Id
            });
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<AuthorPrevDTO>> GetAuthorPreviews(string name = null, string surname = null)
        {
            var filter = new FilterDto();

            List<PredicateDto> predicates = new();

            if (name is not null)
            {
                predicates.Add(new PredicateDto(nameof(AuthorDTO.Name), name, ValueComparingOperator.Contains));
            }

            if (surname is not null)
            {
                predicates.Add(new PredicateDto(nameof(AuthorDTO.Surname), surname, ValueComparingOperator.Contains));
            }

            if (predicates.Count > 0)
            {
                filter.Predicate = new CompositePredicateDto(predicates, LogicalOperator.AND);
            }

            var previews = await _authorPrevService.FilterBy(filter);
            return previews.items;
        }

        public async Task<IEnumerable<AuthorBookDTO>> GetAuthorBooksByAuthor(int id)
        {
            var filter = new FilterDto
            {
                Predicate = new PredicateDto(nameof(AuthorBookDTO.AuthorID), id, ValueComparingOperator.Equal)
            };

            var refsToLoad = new[]
            {
                nameof(AuthorBookDTO.Book),
                nameof(AuthorBookDTO.Author)
            };

            var result = await _authorBookService.FilterBy(filter, refsToLoad);
            return result.items;
        }
    }
}
