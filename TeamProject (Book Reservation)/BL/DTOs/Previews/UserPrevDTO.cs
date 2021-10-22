﻿using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Previews
{
    public class UserPrevDTO : BaseEntityDTO
    {
        [StringLength(32)]
        public string Name { get; set; }

        [StringLength(32)]
        public string Surname { get; set; }
    }
}
