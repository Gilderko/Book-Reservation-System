using BL.DTOs.Entities.Book;
using BL.DTOs.Enums;
using BL.DTOs.Filters;
using BL.Services;
using DAL.Entities;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class BookFacade
    {
        private IUnitOfWork _unitOfWork;
        private ICRUDService<BookDTO, Book> _service;
        private ICRUDService<BookPrevDTO, Book> _bookPrevService;
        private IAuthorService _authorService;
        private IGenreService _genreService;

        public BookFacade(IUnitOfWork unitOfWork,
                          ICRUDService<BookDTO, Book> service,
                          ICRUDService<BookPrevDTO, Book> bookPrevService,
                          IAuthorService authorService,
                          IGenreService genreService)
        {
            _unitOfWork = unitOfWork;
            _service = service;
            _bookPrevService = bookPrevService;
            _authorService = authorService;
            _genreService = genreService;
        }

        public async Task<IEnumerable<BookPrevDTO>> GetBookPreviews(string title,
                                                                    string authorName,
                                                                    string authorSurname,
                                                                    GenreTypeDTO[] genres,
                                                                    LanguageDTO? language,
                                                                    int? pageFrom,
                                                                    int? pageTo,
                                                                    DateTime? releaseFrom,
                                                                    DateTime? releaseTo)
        {
            FilterDto filter = new FilterDto();

            List<PredicateDto> predicates = new();

            if (title is not null)
            {
                predicates.Add(new PredicateDto(nameof(Book.Title), title, Infrastructure.Query.Operators.ValueComparingOperator.Contains));
            }

            if (authorName is not null || authorSurname is not null)
            {
                var bookIds = await _authorService.GetAuthorsBooksIdsByName(authorName, authorSurname);
                predicates.Add(new PredicateDto(nameof(Book.Id), bookIds, Infrastructure.Query.Operators.ValueComparingOperator.In));
            }

            if (genres is not null && genres.Length > 0)
            {
                var bookIds = await _genreService.GetBookIdsByGenres(genres);
                predicates.Add(new PredicateDto(nameof(Book.Id), bookIds, Infrastructure.Query.Operators.ValueComparingOperator.In));
            }

            if (language is not null)
            {
                predicates.Add(new PredicateDto(nameof(Book.Language), (int)language, Infrastructure.Query.Operators.ValueComparingOperator.Contains));
            }

            if (pageFrom is not null)
            {
                predicates.Add(new PredicateDto(nameof(Book.PageCount), (int)pageFrom, Infrastructure.Query.Operators.ValueComparingOperator.GreaterThanOrEqual));
            }

            if (pageTo is not null)
            {
                predicates.Add(new PredicateDto(nameof(Book.PageCount), (int)pageTo, Infrastructure.Query.Operators.ValueComparingOperator.LessThanOrEqual));
            }

            if (releaseFrom is not null)
            {
                predicates.Add(new PredicateDto(nameof(Book.DateOfRelease), releaseFrom, Infrastructure.Query.Operators.ValueComparingOperator.GreaterThanOrEqual));
            }

            if (releaseTo is not null)
            {
                predicates.Add(new PredicateDto(nameof(Book.DateOfRelease), releaseTo, Infrastructure.Query.Operators.ValueComparingOperator.LessThanOrEqual));
            }

            if (predicates.Count > 0)
            {
                filter.Predicate = new CompositePredicateDto(predicates, Infrastructure.Query.Operators.LogicalOperator.AND);
            }

            string[] collectionsToLoad = new string[] {
                nameof(Book.Authors)
            };

            var previews = await _bookPrevService.FilterBy(filter, null, collectionsToLoad);
            await _authorService.LoadAuthors(previews);

            return previews;
        }

        public async Task Create(BookDTO book)
        {
            await _service.Insert(book);
            _unitOfWork.Commit();
        }

        public async Task<BookDTO> Get(int id, string[] refsToLoad = null, string[] collectToLoad = null)
        {
            return await _service.GetByID(id, refsToLoad, collectToLoad);
        }

        public void Update(BookDTO book)
        {
            _service.Update(book);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _service.DeleteById(id);
            _unitOfWork.Commit();
        }
        
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
