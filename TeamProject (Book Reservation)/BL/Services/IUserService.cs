using BL.DTOs.Entities.User;
using DAL.Entities;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IUserService : ICRUDService<UserDTO, User>
    {
        public Task<UserShowDTO> GetUserShowDtoByEmailAsync(string email);

        public Task<UserShowDTO> AuthorizeUserAsync(UserLoginDTO login);

        public Task RegisterUser(UserCreateDTO user);

        public Task<UserEditDTO> GetEditDTO(int id);

        public void UpdateCredentials(UserEditDTO userEdit);
    }
}