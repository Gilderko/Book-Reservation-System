using BL.DTOs.Entities.EBook;
using BL.DTOs.Entities.EReaderInstance;

namespace BL.DTOs.ConnectionTables
{
    public class EBookEReaderInstanceDTO : IEntityDTO
    {
        public int EBookID { get; set; }

        public EBookDTO EBook { get; set; }

        public int EReaderInstanceID { get; set; }

        public EReaderInstanceDTO EReaderInstance { get; set; }
    }
}
