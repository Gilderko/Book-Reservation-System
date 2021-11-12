using DAL.Entities.ConnectionTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.FullVersions
{
    public class BookCollectionDTO : BaseEntityDTO
    {
        [Required]
        [StringLength(128)]
        public string Title { get; set; }
        
        [StringLength(256)]
        public string Description { get; set; }
        
        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public int UserId { get; set; }

        public UserDTO OwnerUser { get; set; }

        // Many to many relationships

        public ICollection<BookCollection_BookDTO> Books { get; set; }
    }
}
