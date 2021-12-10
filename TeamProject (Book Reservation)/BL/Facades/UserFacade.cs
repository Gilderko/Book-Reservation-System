using System;
using BL.DTOs.Entities.BookCollection;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.EReaderInstance;
using BL.DTOs.Entities.User;
using BL.Services;
using DAL.Entities;
using System.Threading.Tasks;
using Infrastructure;
using System.IO;
using System.Collections.Generic;
using BL.DTOs.Filters;
using Infrastructure.Query.Operators;

namespace BL.Facades
{
    public class UserFacade
    {
        private IUnitOfWork _unitOfWork;
        private IUserService _userService;
        private ICRUDService<BookCollectionDTO, BookCollection> _bookCollCrud;
        private ICRUDService<BookInstanceDTO, BookInstance> _bookInstanceCrud;
        private ICRUDService<EReaderInstanceDTO, EReaderInstance> _eReaderInstanceCrud;

        public UserFacade(IUnitOfWork unitOfWork,
            IUserService userService,
            ICRUDService<BookCollectionDTO, BookCollection> bookCollCrud,
            ICRUDService<BookInstanceDTO, BookInstance> bookInstanceCrud,
            ICRUDService<EReaderInstanceDTO, EReaderInstance> eReaderInstanceCrud)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _bookCollCrud = bookCollCrud;
            _bookInstanceCrud = bookInstanceCrud;
            _eReaderInstanceCrud = eReaderInstanceCrud;
        }

        public async Task Create(UserDTO user)
        {
            await _userService.Insert(user);
            _unitOfWork.Commit();
        }

        public async Task<UserDTO> Get(int id, string[] refsToLoad = null, string[] collectToLoad = null)
        {
            return await _userService.GetByID(id, refsToLoad, collectToLoad);
        }

        public async Task Update(UserDTO user)
        {
            if (await _userService.GetUserShowDtoByEmailAsync(user.Email) != null)
            {
                throw new ArgumentException("User with this email already exist!");
            }

            _userService.Update(user);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _userService.DeleteById(id);
            _unitOfWork.Commit();
        }

        public async Task<(IEnumerable<UserDTO>,int)> GetAllUsers()
        {
            var simplePredicate = new PredicateDto(nameof(UserDTO.Id), 1, ValueComparingOperator.GreaterThanOrEqual);

            var filter = new FilterDto()
            {
                Predicate = simplePredicate
            };

            return await _userService.FilterBy(filter);
        }

        public async Task<UserEditDTO> GetEditDTO(int id)
        {
            return await _userService.GetEditDTO(id);
        }

        public void UpdateCredentials(UserEditDTO userEdit)
        {
            _userService.UpdateCredentials(userEdit);
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
        
        public async Task<UserShowDTO> LoginAsync(UserLoginDTO userLogin)
        {
            var user = await _userService.AuthorizeUserAsync(userLogin);
            if (user != null)
            {
                return user;
            }
            throw new UnauthorizedAccessException();
        }

        public async Task RegisterUserAsync(UserCreateDTO user)
        {
            // checks if user with this email already exists
            if (await _userService.GetUserShowDtoByEmailAsync(user.Email) != null)
            {
                throw new ArgumentException("User with this email already exist!");
            }
            
            await _userService.RegisterUser(user);
            _unitOfWork.Commit();
        }
    }
}
