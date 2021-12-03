using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.Author
{
    public class AuthorPrevDTO : BaseEntityDTO
    {
        [StringLength(32)]
        public string Name { get; set; }

        [StringLength(32)]
        public string Surname { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }
    }
}
