﻿using BL.DTOs.ConnectionTables;
using DAL.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.FullVersions
{
    public class BookInstanceDTO : BaseEntityDTO
    {
        [Required]
        public BookInstanceCondition Conditon { get; set; }

        [Required]
        public int BookOwnerId { get; set; }

        public UserDTO Owner { get; set; }

        [Required]
        public int BookTemplateID { get; set; }

        public BookDTO FromBookTemplate { get; set; }

        public ICollection<Reservation_BookInstanceDTO> AllReservations { get; set; }
    }
}
