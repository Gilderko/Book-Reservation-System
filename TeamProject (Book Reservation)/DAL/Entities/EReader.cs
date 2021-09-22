using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class EReader : BaseEntity
    {
        [MaxLength(64)]
        public string Model { get; set; }

        public int MemoryInMB { get; set; }

        public ICollection<EBookInstance> BooksIncluded { get; set; }
    }
}
