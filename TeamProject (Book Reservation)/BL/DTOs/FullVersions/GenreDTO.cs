using DAL.Entities.ConnectionTables;
using System.Collections.Generic;

namespace BL.DTOs.FullVersions
{
    public class GenreDTO : BaseEntityDTO
    {
        // Many to many relationships
        public ICollection<Book_GenreDTO> Books { get; set; }
    }
}
