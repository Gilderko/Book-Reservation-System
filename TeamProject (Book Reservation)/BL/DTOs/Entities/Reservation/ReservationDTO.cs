using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.EReaderInstance;
using BL.DTOs.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.Reservation
{
    public class ReservationDTO : BaseEntityDTO
    {
        [Required]
        [DisplayName("From")]
        public DateTime DateFrom { get; set; }

        [Required]
        [DisplayName("To")]
        public DateTime DateTill { get; set; }

        public int? UserID { get; set; }

        public UserDTO User { get; set; }

        public int? EReaderID { get; set; }

        [DisplayName("E-Reader")]
        public EReaderInstanceDTO EReader { get; set; }

        // Many to many relationships
        [DisplayName("Books")]
        public ICollection<ReservationBookInstanceDTO> BookInstances { get; set; }
    }
}
