using BL.Services;
using DAL.Entities;
using Infrastructure;
using System;
using System.Collections.Generic;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.BookInstance;
using System.Threading.Tasks;
using BL.DTOs.Filters;
using BL.DTOs.Enums;

namespace BL.Facades
{
    public class BookFacade
    {
        private IUnitOfWork _unitOfWork;
        private ICRUDService<BookDTO, Book> _service;
        private IBookPreviewService _bookPrevService;

        public BookFacade(IUnitOfWork unitOfWork,
                          ICRUDService<BookDTO, Book> service,
                          IBookPreviewService bookPrevService)
        {
            _unitOfWork = unitOfWork;
            _service = service;
            _bookPrevService = bookPrevService;
        }

        public async Task<IEnumerable<BookPrevDTO>> GetBookPreviews(string title,
                                                                    string author,
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

            if (author is not null)
            {
                
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



            return await _bookPrevService.GetBookPreviewsByFilter(filter);
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
