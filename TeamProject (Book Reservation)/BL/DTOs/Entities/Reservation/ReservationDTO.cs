using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.EReaderInstance;
using BL.DTOs.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.Reservation
{
    public class ReservationDTO : BaseEntityDTO
    {
        [Required]
        public DateTime DateFrom { get; set; }

        [Required]
        public DateTime DateTill { get; set; }

        [Required]
        public int UserID { get; set; }

        public UserDTO User { get; set; }

        public int? EReaderID { get; set; }

        public EReaderInstanceDTO EReader { get; set; }

        // Many to many relationships

        public ICollection<ReservationBookInstanceDTO> BookInstances { get; set; }
    }
}
