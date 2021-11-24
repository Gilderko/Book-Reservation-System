using BL.DTOs.Entities.BookCollection;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.EReaderInstance;
using BL.DTOs.Entities.User;
using BL.Services;
using DAL.Entities;
using System.Threading.Tasks;
using Infrastructure;

namespace BL.Facades
{
    public class UserFacade
    {
        private IUnitOfWork _unitOfWork;
        private ICRUDService<UserDTO, User> _userCrud;
        private ICRUDService<BookCollectionDTO, BookCollection> _bookCollCrud;
        private ICRUDService<BookInstanceDTO, BookInstance> _bookInstanceCrud;
        private ICRUDService<EReaderInstanceDTO, EReaderInstance> _eReaderInstanceCrud;

        public UserFacade(IUnitOfWork unitOfWork,
            ICRUDService<UserDTO, User> userCrud,
            ICRUDService<BookCollectionDTO, BookCollection> bookCollCrud,
            ICRUDService<BookInstanceDTO, BookInstance> bookInstanceCrud,
            ICRUDService<EReaderInstanceDTO, EReaderInstance> eReaderInstanceCrud)
        {
            _unitOfWork = unitOfWork;
            _userCrud = userCrud;
            _bookCollCrud = bookCollCrud;
            _bookInstanceCrud = bookInstanceCrud;
            _eReaderInstanceCrud = eReaderInstanceCrud;
        }

        public async Task Create(UserDTO user)
        {
            await _userCrud.Insert(user);
            _unitOfWork.Commit();
        }

        public async Task<UserDTO> Get(int id)
        {
            return await _userCrud.GetByID(id);
        }

        public void Update(UserDTO user)
        {
            _userCrud.Update(user);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _userCrud.DeleteById(id);
            _unitOfWork.Commit();
        }

        public async Task AddBookCollection(int authorId, BookCollectionDTO bookCollection)
        {
            bookCollection.UserId = authorId;
            await _bookCollCrud.Insert(bookCollection);
            _unitOfWork.Commit();
        }

        public async Task AddBookInstance(int ownerId, BookInstanceDTO bookInstance)
        {
            bookInstance.BookOwnerId = ownerId;
            await _bookInstanceCrud.Insert(bookInstance);
            _unitOfWork.Commit();
        }
        
        public async Task AddEreaderInstance(int ownerId, EReaderInstanceDTO eReaderInstance)
        {
            eReaderInstance.EreaderOwnerId = ownerId;
            await _eReaderInstanceCrud.Insert(eReaderInstance);
            _unitOfWork.Commit();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
