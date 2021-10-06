using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class EReader : BaseEntity
    {
        [MaxLength(64)]
        public string Model { get; set; }

        [MaxLength(64)]
        public string CompanyMake { get; set; }

        public int MemoryInMB { get; set; }
    }
}
