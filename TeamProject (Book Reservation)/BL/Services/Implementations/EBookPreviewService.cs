using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.EBook;
using BL.DTOs.Filters;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;
using System.Threading.Tasks;

namespace BL.Services.Implementations
{
    public class EBookPreviewService : CRUDService<EBookPrevDTO, EBook>, 
        IEBookPreviewService
    {
        private QueryObject<EBookPrevDTO, EBook> _resQueryObject;
        
        public EBookPreviewService(IRepository<EBook> repo, 
                                   IMapper mapper, 
                                   QueryObject<EBookPrevDTO, EBook> resQueryObject) : base(repo, mapper)
        {
            _resQueryObject = resQueryObject;
        }

        public async Task<IEnumerable<BookPrevDTO>> GetEBookPrevsByFilter(FilterDto filter)
        {
            return (await _resQueryObject.ExecuteQuery(filter)).Items;
        }
    }
}