using DAL.Entities.ConnectionTables;
using System;
using System.Collections.Generic;

namespace BL.DTOs.FullVersions
{
    public class ReservationDTO : BaseEntityDTO
    {
        public DateTime DateFrom { get; set; }

        public DateTime DateTill { get; set; }

        public int UserID { get; set; }

        public UserDTO User { get; set; }

        public int? EReaderID { get; set; }

        public EReaderInstanceDTO EReader { get; set; }

        // Many to many relationships

        public ICollection<Reservation_BookInstanceDTO> BookInstances { get; set; }
    }
}
