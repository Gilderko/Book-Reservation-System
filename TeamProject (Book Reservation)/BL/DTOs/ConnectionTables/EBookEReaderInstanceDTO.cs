using System.ComponentModel.DataAnnotations.Schema;
using BL.DTOs.Entities.EBook;
using BL.DTOs.Entities.EReaderInstance;

namespace BL.DTOs.ConnectionTables
{
    public class EBookEReaderInstanceDTO : IEntityDTO
    {
        public int EBookID { get; set; }

        public EBookDTO EBook { get; set; }

        public int EReaderID { get; set; }

        public EReaderInstanceDTO EReader { get; set; }
    }
}
