using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public abstract class BaseEntityDTO : IEntityDTO
    {
        // The name of property Id is hardcoded into QueryBase
        [Key]
        public int Id { get; set; }
    }
}
