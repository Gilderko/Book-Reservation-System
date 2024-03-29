﻿using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.EReader;
using BL.DTOs.Entities.Reservation;
using BL.DTOs.Entities.User;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.EReaderInstance
{
    public class EReaderInstanceDTO : BaseEntityDTO
    {
        [Required]
        [StringLength(1024)]
        public string Description { get; set; }

        [Required]
        [DisplayName("E-Reader id")]
        public int EReaderTemplateID { get; set; }

        [Required]
        [DisplayName("Owner id")]
        public int EreaderOwnerId { get; set; }

        public UserDTO Owner { get; set; }

        [DisplayName("E-Reader")]
        public EReaderDTO EReaderTemplate { get; set; }

        [DisplayName("Books")]
        public IEnumerable<EBookEReaderInstanceDTO> BooksIncluded { get; set; }

        public ICollection<ReservationDTO> Reservations { get; set; }
    }
}
