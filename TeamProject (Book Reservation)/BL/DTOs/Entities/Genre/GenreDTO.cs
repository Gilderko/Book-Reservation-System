using BL.DTOs.ConnectionTables;
using System.Collections.Generic;

namespace BL.DTOs.Entities.Genre
{
    public class GenreDTO : BaseEntityDTO
    {
        // Many to many relationships
        public ICollection<BookGenreDTO> Books { get; set; }
    }
}
