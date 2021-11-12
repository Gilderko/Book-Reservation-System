using BL.DTOs.FullVersions;
using System.ComponentModel.DataAnnotations.Schema;

namespace BL.DTOs.ConnectionTables
{
    public class EBook_EReaderInstanceDTO
    {
        public int EBookID { get; set; }

        public EBookDTO EBook { get; set; }

        public int EReaderID { get; set; }

        public EReaderDTO EReader { get; set; }
    }
}
