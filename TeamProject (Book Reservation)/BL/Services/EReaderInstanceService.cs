using System.Collections.Generic;
using AutoMapper;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.EBook;
using BL.DTOs.Entities.EReaderInstance;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;

namespace BL.Services
{
    public class EReaderInstanceService<TEntityDTO, TEntity> : CRUDService<TEntityDTO, TEntity>, 
        IEReaderInstanceService<TEntityDTO, TEntity> where TEntity : EReaderInstance
                                                     where TEntityDTO : EReaderInstanceDTO
    {
        private QueryObject<EReaderInstanceDTO, EReaderInstance> _resQueryObject;
        private IMapper _mapper;

        public EReaderInstanceService(IRepository<TEntity> repo, 
                                      Mapper mapper, 
                                      QueryObject<EReaderInstanceDTO, EReaderInstance> resQueryObject) : base(repo, mapper)
        {
            _mapper = mapper;
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