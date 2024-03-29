﻿using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.BookCollection
{
    public class BookCollectionDTO : BaseEntityDTO
    {
        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Created on")]
        public DateTime CreationDate { get; set; }

        [Required]
        [DisplayName("User id")]
        public int UserId { get; set; }

        [DisplayName("Owner")]
        public UserDTO OwnerUser { get; set; }

        // Many to many relationships

        public ICollection<BookCollectionBookDTO> Books { get; set; }
    }
}
