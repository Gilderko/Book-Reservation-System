using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class EReaderTemplate : BaseEntity
    {
        [MaxLength(64)]
        public string Model { get; set; }

        [MaxLength(64)]
        public string CompanyMake { get; set; }

        public int MemoryInMB { get; set; }
    }
}
