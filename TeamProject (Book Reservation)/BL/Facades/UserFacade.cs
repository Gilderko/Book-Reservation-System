using BL.DTOs.Entities.BookCollection;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.EReaderInstance;
using BL.DTOs.Entities.User;
using BL.Services;
using DAL.Entities;
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

        public void Create(UserDTO user)
        {
            _userCrud.Insert(user);
            _unitOfWork.Commit();
        }

        public UserDTO Get(int id)
        {
            return _userCrud.GetByID(id);
        }

        public void Update(UserDTO user)
        {
            _userCrud.Update(user);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _userCrud.Delete(id);
            _unitOfWork.Commit();
        }

        public void AddBookCollection(int authorId, BookCollectionDTO bookCollection)
        {
            bookCollection.UserId = authorId;
            _bookCollCrud.Insert(bookCollection);
            _unitOfWork.Commit();
        }

        public void AddBookInstance(int ownerId, BookInstanceDTO bookInstance)
        {
            bookInstance.BookOwnerId = ownerId;
            _bookInstanceCrud.Insert(bookInstance);
            _unitOfWork.Commit();
        }
        
        public void AddEreaderInstance(int ownerId, EReaderInstanceDTO eReaderInstance)
        {
            eReaderInstance.EreaderOwnerId = ownerId;
            _eReaderInstanceCrud.Insert(eReaderInstance);
            _unitOfWork.Commit();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
