using BL.DTOs.ConnectionTables;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.Author
{
    public class AuthorDTO : BaseEntityDTO
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [StringLength(32)]
        public string Surname { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }

        // Many to many relationships

        public IEnumerable<AuthorBookDTO> AuthorsBooks { get; set; }
    }
}
