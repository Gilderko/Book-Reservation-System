using DAL.Entities.ConnectionTables;
using System.Collections.Generic;

namespace BL.DTOs.FullVersions
{
    public class BookInstanceDTO : BaseEntityDTO
    {
        public int BookTemplateID { get; set; }

        public BookDTO FromBookTemplate { get; set; }

        public ICollection<Reservation_BookInstanceDTO> AllReservations { get; set; }
    }
}
