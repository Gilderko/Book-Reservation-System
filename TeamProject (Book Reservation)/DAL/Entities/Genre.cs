using DAL.Entities.ConnectionTables;
using System.Collections.Generic;

namespace DAL.Entities
{

    public class Genre : BaseEntity
    {
        // Many to many relationships

        public ICollection<Book_Genre> Books { get; set; }
    }
}
