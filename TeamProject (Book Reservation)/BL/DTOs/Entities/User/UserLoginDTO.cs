using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.User
{
    public class UserLoginDTO
    {
        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }
    }
}