using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class BaseEntity 
    {
        // The name of property Id is hardcoded into QueryBase
        [Key]
        public int Id { get; set; }
    }
}
