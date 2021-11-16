using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.EBook;
using BL.DTOs.Filters;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;

namespace BL.Services
{
    public class EBookPreviewService<TEntityDTO, TEntity> : CRUDService<TEntityDTO, TEntity>, 
        IEBookPreviewService<TEntityDTO, TEntity> where TEntity : EBook
                                                  where TEntityDTO : EBookPrevDTO
    {
        private QueryObject<EBookPrevDTO, EBook> _resQueryObject;
        
        public EBookPreviewService(IRepository<TEntity> repo, 
                                   IMapper mapper, 
                                   QueryObject<EBookPrevDTO, EBook> resQueryObject) : base(repo, mapper)
        {
            _resQueryObject = resQueryObject;
        }

        public IEnumerable<BookPrevDTO> GetEBookPrevsByFilter(FilterDto filter)
        {
            return _resQueryObject.ExecuteQuery(filter).Items;
        }
    }
}