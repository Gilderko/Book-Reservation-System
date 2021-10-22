using DAL.Entities.ConnectionTables;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.FullVersions
{
    public class AuthorDTO : BaseEntityDTO
    {
        [StringLength(32)]
        public string Name { get; set; }

        [StringLength(32)]
        public string Surname { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }

        // Many to many relationships

        public ICollection<Author_BookDTO> AuthorsBooks { get; set; }
    }
}
