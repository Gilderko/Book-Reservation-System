using AutoMapper;
using BL.DTOs.Entities.BookInstance;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;
using System.Threading.Tasks;

namespace BL.Services.Implementations
{
    public class BookInstanceService : CRUDService<BookInstanceDTO, BookInstance>, IBookInstanceService
    {
        public BookInstanceService(IRepository<BookInstance> repo, IMapper mapper,
            QueryObject<BookInstanceDTO, BookInstance> queryObject) : base(repo, mapper, queryObject)
        {
        }

        public async Task CreateBookInstance(int ownerId, int bookTemplateId, BookInstanceCreateDTO createBookInstance)
        {
            createBookInstance.BookOwnerId = ownerId;
            createBookInstance.BookTemplateID = bookTemplateId;
            BookInstanceDTO newBook = Mapper.Map<BookInstanceDTO>(createBookInstance);
            await Insert(newBook);
        }
    }
}
