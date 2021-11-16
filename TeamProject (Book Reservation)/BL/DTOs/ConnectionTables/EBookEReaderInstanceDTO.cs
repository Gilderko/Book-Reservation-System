using BL.DTOs.FullVersions;
using System.ComponentModel.DataAnnotations.Schema;

namespace BL.DTOs.ConnectionTables
{
    public class EBookEReaderInstanceDTO
    {
        public int EBookID { get; set; }

        public EBookDTO EBook { get; set; }

        public int EReaderID { get; set; }

        public EReaderInstanceDTO EReader { get; set; }
    }
}
