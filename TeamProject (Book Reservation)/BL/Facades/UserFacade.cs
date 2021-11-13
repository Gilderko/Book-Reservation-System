using BL.DTOs.FullVersions;
using BL.Services;
using DAL.Entities;
using Infrastructure;

namespace BL.Facades
{
    public class UserFacade
    {
        private IUnitOfWork _unitOfWork;
        private CRUDService<UserDTO, User> _userCrud;
        private CRUDService<BookCollectionDTO, BookCollection> _bookCollCrud;
        private CRUDService<BookInstanceDTO, BookInstance> _bookInstanceCrud;
        private CRUDService<EReaderInstanceDTO, EReaderInstance> _eReaderInstanceCrud;

        public UserFacade(IUnitOfWork unitOfWork, 
            CRUDService<UserDTO, User> userCrud, 
            CRUDService<BookCollectionDTO, BookCollection> bookCollCrud,
            CRUDService<BookInstanceDTO, BookInstance> bookInstanceCrud,
            CRUDService<EReaderInstanceDTO, EReaderInstance> eReaderInstanceCrud)
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
