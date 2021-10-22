using DAL.Entities.ConnectionTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.FullVersions
{
    public class BookCollectionDTO : BaseEntityDTO
    {
        [StringLength(128)]
        public string Title { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }

        public UserDTO OwnerUser;

        // Many to many relationships

        public ICollection<BookCollection_BookDTO> Books { get; set; }
    }
}
