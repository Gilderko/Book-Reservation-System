using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class EBookTemplate : BookTemplate
    {
        public int MemorySize { get; set; }

        public EBookFormat Format { get; set; }

        public ICollection<EBookInstance> EBookInstances { get; set; }
    }

    public enum EBookFormat
    {
        EPUB, PDF, MOBI
    }
}
