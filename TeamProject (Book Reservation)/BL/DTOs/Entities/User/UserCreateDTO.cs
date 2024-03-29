using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.User
{
    public class UserCreateDTO : BaseEntityDTO
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [Required]
        [StringLength(32)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        public string HashedPassword { get; set; }

        [Required]
        public bool IsAdmin { get; set; }
    }
}