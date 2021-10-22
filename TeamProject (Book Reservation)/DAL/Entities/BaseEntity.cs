using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public abstract class BaseEntity : IEntity
    {
        // The name of property Id is hardcoded into QueryBase
        [Key]
        public int Id { get; set; }
    }
}
