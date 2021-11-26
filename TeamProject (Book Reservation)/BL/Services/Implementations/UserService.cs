using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs.Entities.User;
using BL.DTOs.Filters;
using BL.QueryObjects;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query.Operators;

namespace BL.Services.Implementations
{
    public class UserService : CRUDService<UserDTO, User>, IUserService
    {
        private QueryObject<UserShowDTO, User> _resQueryObject;
        private QueryObject<UserEditDTO, User> _resEditQueryObject;
        private const int PBKDF2IterCount = 100000;
        private const int PBKDF2SubkeyLength = 160 / 8;
        private const int saltSize = 128 / 8;
        
        public UserService(IRepository<User> repo, IMapper mapper, QueryObject<UserShowDTO, User> query, 
            QueryObject<UserEditDTO, User> queryEdit) : base (repo, mapper)
        {
            _resQueryObject = query;
            _resEditQueryObject = queryEdit;
        }

        public async Task<UserShowDTO> GetUserShowDtoByEmailAsync(string email)
        {
            FilterDto filter = new FilterDto()
            {
                Predicate = new PredicateDto(nameof(User.Email), email, ValueComparingOperator.Equal)
            };

            var result = await _resQueryObject.ExecuteQuery(filter);

            if (result.TotalItemsCount > 0)
            {
                return result.Items.ToArray()[0];
            }

            return null;
        }
        
        public async Task<UserShowDTO> AuthorizeUserAsync(UserLoginDTO login)
        {
            // get userId
            UserShowDTO userDto = await GetUserShowDtoByEmailAsync(login.Email);

            if (userDto == null)
            {
                return null;
            }

            //get user entity
            var user = await GetByID(userDto.Id);

            var (hash, salt) = user != null ? GetPassAndSalt(user.HashedPassword) : (string.Empty, string.Empty);

            var succ = user != null && VerifyHashedPassword(hash, salt, login.Password);
            return succ ? userDto : null;
        }

        public async Task RegisterUser(UserCreateDTO user)
        {
            var (hash, salt) = CreateHash(user.Password);
            user.HashedPassword = string.Join(',', hash, salt);

            UserDTO userDto = new UserDTO();
            Mapper.Map(user, userDto);
            
            await Insert(userDto);
        }

        public async Task<UserEditDTO> GetEditDTO(int id)
        {
            FilterDto filter = new FilterDto()
            {
                Predicate = new PredicateDto(nameof(User.Id), id, ValueComparingOperator.Equal)
            };

            var result = await _resEditQueryObject.ExecuteQuery(filter);

            if (result.TotalItemsCount > 0)
            {
                return result.Items.ToArray()[0];
            }

            return null;
        }

        public void UpdateCredentials(UserEditDTO userEdit)
        {
            var (hash, salt) = CreateHash(userEdit.Password);
            userEdit.HashedPassword = string.Join(',', hash, salt);

            UserDTO userDto = new UserDTO();
            Mapper.Map(userEdit, userDto);

            Update(userDto);
        }

        private (string, string) GetPassAndSalt(string passwordHash)
        {
            var result = passwordHash.Split(',');
            if (result.Count() != 2)
            {
                return (string.Empty, string.Empty);
            }
            return (result[0], result[1]);
        }

        private bool VerifyHashedPassword(string hashedPassword, string salt, string password)
        {
            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            var saltBytes = Convert.FromBase64String(salt);

            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, PBKDF2IterCount))
            {
                var generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                return hashedPasswordBytes.SequenceEqual(generatedSubkey);
            }
        }

        private Tuple<string, string> CreateHash(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                return Tuple.Create(Convert.ToBase64String(subkey), Convert.ToBase64String(salt));
            }
        }
    }
}