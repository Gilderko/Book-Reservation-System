using System.ComponentModel;

namespace BL.DTOs.Entities.User
{
    public class UserShowDTO: BaseEntityDTO
    {
        public string Email { get; set; }

        [DisplayName("Admin")]
        public bool IsAdmin { get; set; }
    }
}