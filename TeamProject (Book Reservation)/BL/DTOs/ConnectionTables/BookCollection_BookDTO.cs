using BL.DTOs;
using BL.DTOs.FullVersions;

namespace DAL.Entities.ConnectionTables
{
    public class BookCollection_BookDTO : IEntityDTO
    {
        public int BookCollectionID { get; set; }


        public BookCollectionDTO BookCollect { get; set; }

        public int BookID { get; set; }


        public BookDTO Book { get; set; }
    }
}
