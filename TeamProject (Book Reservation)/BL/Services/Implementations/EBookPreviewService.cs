using AutoMapper;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.EBook;
using BL.DTOs.Filters;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services.Implementations
{
    public class EBookPreviewService : CRUDService<EBookPrevDTO, EBook>,
        IEBookPreviewService
    {

        public EBookPreviewService(IRepository<EBook> repo,
                                   IMapper mapper,
                                   QueryObject<EBookPrevDTO, EBook> resQueryObject) : base(repo, mapper, resQueryObject)
        {

        }

        public async Task<IEnumerable<BookPrevDTO>> GetEBookPrevsByFilter(FilterDto filter)
        {
            var result = await FilterBy(filter);
            return result.items;
        }
    }
}