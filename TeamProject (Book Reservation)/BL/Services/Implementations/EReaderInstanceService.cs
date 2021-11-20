using System.Collections.Generic;
using AutoMapper;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.EBook;
using BL.DTOs.Entities.EReaderInstance;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;

namespace BL.Services.Implementations
{
    public class EReaderInstanceService : CRUDService<EReaderInstanceDTO, EReaderInstance>, IEReaderInstanceService
    {
        private QueryObject<EReaderInstanceDTO, EReaderInstance> _resQueryObject;

        public EReaderInstanceService(IRepository<EReaderInstance> repo, 
                                      IMapper mapper, 
                                      QueryObject<EReaderInstanceDTO, EReaderInstance> resQueryObject) : base(repo, mapper)
        {
            _resQueryObject = resQueryObject;
        }

        public void AddEBook(EReaderInstanceDTO eReaderInstance, EBookDTO eBook)
        {
            EBookEReaderInstanceDTO eBookEReaderInstanceDto = new EBookEReaderInstanceDTO()
            {
                EBook = eBook,
                EReader = eReaderInstance
            };
            
            ((List<EBookEReaderInstanceDTO>) eReaderInstance.BooksIncluded).Add(eBookEReaderInstanceDto);
        }
        
        public void DeleteEBook(EReaderInstanceDTO eReaderInstance, EBookDTO eBook)
        {
            var eBookList = ((List<EBookEReaderInstanceDTO>) eReaderInstance.BooksIncluded);
            
            eBookList.Remove(eBookList.Find(x => x.EBook.Id == eBook.Id));
        }
    }
}