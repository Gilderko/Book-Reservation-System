using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using BL.DTOs.Filters;
using BL.DTOs.Previews;
using BL.QueryObjects;
using DAL.Entities;

namespace BL.Services
{
    public class EBookPreviewService
    {
        private IMapper _mapper;
        private QueryObject<EBookPrevDTO, EBook> _resQueryObject;
        
        public EBookPreviewService(IMapper mapper, QueryObject<EBookPrevDTO, EBook> resQueryObject)
        {
            _mapper = mapper;
            _resQueryObject = resQueryObject;
        }

        public IEnumerable<BookPrevDTO> GetEBookPrevsByFilter(FilterDto filter)
        {
            return _resQueryObject.ExecuteQuery(filter).Items;
        }
    }
}