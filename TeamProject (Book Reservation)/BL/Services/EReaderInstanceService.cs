using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Filters;
using BL.DTOs.FullVersions;
using BL.DTOs.Previews;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure.Query.Operators;

namespace BL.Services
{
    public class EReaderInstanceService
    {
        private QueryObject<EReaderInstanceDTO, EReaderInstance> _resQueryObject;
        private IMapper _mapper;

        public EReaderInstanceService(IMapper mapper, QueryObject<EReaderInstanceDTO, EReaderInstance> resQueryObject)
        {
            _mapper = mapper;
            _resQueryObject = resQueryObject;
        }

        public void AddEBook(EReaderInstanceDTO eReaderInstance, EBookDTO eBook)
        {
            EBook_EReaderInstanceDTO eBookEReaderInstanceDto = new EBook_EReaderInstanceDTO()
            {
                EBook = eBook,
                EReader = eReaderInstance
            };
            
            ((List<EBook_EReaderInstanceDTO>) eReaderInstance.BooksIncluded).Add(eBookEReaderInstanceDto);
        }
        
        public void DeleteEBook(EReaderInstanceDTO eReaderInstance, EBookDTO eBook)
        {
            var eBookList = ((List<EBook_EReaderInstanceDTO>) eReaderInstance.BooksIncluded);
            
            eBookList.Remove(eBookList.Find(x => x.EBook.Id == eBook.Id));
        }
    }
}