using BL.DTOs.Enums;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.BookInstance
{
    public class BookInstanceCreateDTO : BaseEntityDTO
    {
        [Required]
        public BookInstanceConditionDTO Condition { get; set; }

        public int BookOwnerId { get; set; }

        public int BookTemplateID { get; set; }
    }
}
