namespace BL.DTOs.Entities.User
{
    public class UserShowDTO: BaseEntityDTO
    {
        public string Email { get; set; }
        
        public bool IsAdmin { get; set; }
    }
}