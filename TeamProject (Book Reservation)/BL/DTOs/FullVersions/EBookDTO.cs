using DAL.Enums;
using System.Collections.Generic;

namespace BL.DTOs.FullVersions
{
    public class EBookDTO : BookDTO
    {
        public int MemorySize { get; set; }

        public EBookFormat Format { get; set; }

        public ICollection<EBookInstanceDTO> EBookInstances { get; set; }
    }


}
