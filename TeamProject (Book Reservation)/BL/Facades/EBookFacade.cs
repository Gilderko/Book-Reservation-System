using BL.DTOs.Entities.EBook;
using System.Threading.Tasks;
using BL.Services;
using DAL.Entities;
using Infrastructure;
using System.Collections.Generic;
using BL.DTOs.Enums;
using System;
using BL.DTOs.Filters;
using Infrastructure.Query.Operators;

namespace BL.Facades
{
    public class EBookFacade
    {
        private IUnitOfWork _unitOfWork;
        private ICRUDService<EBookDTO, EBook> _service;
        private ICRUDService<EBookPrevDTO, EBook> _eBookPrevService;
        private IAuthorService _authorService;
        private IGenreService _genreService;

        public EBookFacade(IUnitOfWork unitOfWork, 
                           ICRUDService<EBookDTO, EBook> service, 
                           IAuthorService authorService, 
                           IGenreService genreService, 
                           ICRUDService<EBookPrevDTO, EBook> eBookPrevService)
        {
            _unitOfWork = unitOfWork;
            _service = service;
            _authorService = authorService;
            _genreService = genreService;
            _eBookPrevService = eBookPrevService;
        }

        public async Task<IEnumerable<EBookPrevDTO>> GetBookPreviews(string title,
                                                                   string authorName,
                                                                   string authorSurname,
                                                                   GenreTypeDTO[] genres,
                                                                   LanguageDTO? language,
                                                                   int? pageFrom,
                                                                   int? pageTo,
                                                                   DateTime? releaseFrom,
                                                                   DateTime? releaseTo,
                                                                   EBookFormatDTO? format)
        {
            FilterDto filter = new FilterDto();

            List<PredicateDto> predicates = new();

            if (title is not null)
            {
                predicates.Add(new PredicateDto(nameof(Book.Title), title, ValueComparingOperator.Contains));
            }

            if (authorName is not null || authorSurname is not null)
            {
                var bookIds = await _authorService.GetAuthorsBooksIdsByName(authorName, authorSurname);
                predicates.Add(new PredicateDto(nameof(Book.Id), bookIds, ValueComparingOperator.In));
            }

            if (genres is not null && genres.Length > 0)
            {
                var bookIds = await _genreService.GetBookIdsByGenres(genres);
                predicates.Add(new PredicateDto(nameof(Book.Id), bookIds, ValueComparingOperator.In));
            }

            if (language is not null)
            {
                predicates.Add(new PredicateDto(nameof(EBook.Language), (int)language, ValueComparingOperator.Contains));
            }

            if (pageFrom is not null)
            {
                predicates.Add(new PredicateDto(nameof(EBook.PageCount), (int)pageFrom, ValueComparingOperator.GreaterThanOrEqual));
            }

            if (pageTo is not null)
            {
                predicates.Add(new PredicateDto(nameof(EBook.PageCount), (int)pageTo, ValueComparingOperator.LessThanOrEqual));
            }

            if (releaseFrom is not null)
            {
                predicates.Add(new PredicateDto(nameof(EBook.DateOfRelease), releaseFrom, ValueComparingOperator.GreaterThanOrEqual));
            }

            if (releaseTo is not null)
            {
                predicates.Add(new PredicateDto(nameof(EBook.DateOfRelease), releaseTo, ValueComparingOperator.LessThanOrEqual));
            }

            if (format is not null)
            {
                predicates.Add(new PredicateDto(nameof(EBook.Format), (int)format, ValueComparingOperator.Equal));
            }

            if (predicates.Count > 0)
            {
                filter.Predicate = new CompositePredicateDto(predicates, LogicalOperator.AND);
            }

            string[] collectionsToLoad = new string[] {
                nameof(Book.Authors)
            };

            var previews = await _eBookPrevService.FilterBy(filter, null, collectionsToLoad);
            await _authorService.LoadAuthors(previews);

            return previews;
        }

        public async Task Create(EBookDTO eBook)
        {
            await _service.Insert(eBook);
            _unitOfWork.Commit();
        }

        public async Task<EBookDTO> Get(int id, string[] refsToLoad = null, string[] collectToLoad = null)
        {
            return await _service.GetByID(id, refsToLoad, collectToLoad);
        }

        public void Update(EBookDTO eBook)
        {
            _service.Update(eBook);
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
