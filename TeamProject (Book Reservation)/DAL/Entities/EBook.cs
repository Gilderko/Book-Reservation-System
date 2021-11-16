using DAL.Entities.ConnectionTables;
using DAL.Enums;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class EBook : Book
    {
        public int MemorySize { get; set; }

        public EBookFormat Format { get; set; }

        public ICollection<EBookEReaderInstance> EReaders { get; set; }
    }
}
