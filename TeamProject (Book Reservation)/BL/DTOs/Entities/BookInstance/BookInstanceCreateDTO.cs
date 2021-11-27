using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.User;
using BL.DTOs.Enums;
using DAL.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.BookInstance
{
    public class BookInstanceCreateDTO : BaseEntityDTO
    {
        [Required]
        public BookInstanceConditionDTO Condition { get; set; }

        public int BookOwnerId { get; set; }

        [Required]
        public int BookTemplateID { get; set; }
    }
}
