using System.Threading.Tasks;
using BL.DTOs.Entities.User;
using DAL.Entities;

namespace BL.Services
{
    public interface IUserService : ICRUDService<UserDTO, User>
    {
        Task<UserShowDTO> GetUserShowDtoByEmailAsync(string email);
        
        Task<UserShowDTO> AuthorizeUserAsync(UserLoginDTO login);
        
        Task RegisterUser(UserCreateDTO user);
    }
}