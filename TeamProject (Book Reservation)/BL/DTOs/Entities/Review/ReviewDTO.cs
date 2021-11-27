using BL.DTOs.Entities.Book;
using BL.DTOs.Entities.User;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.Review
{
    public class ReviewDTO : BaseEntityDTO
    {
        [Required]
        [DisplayName("Created at")]
        public DateTime CreationDate { get; set; }

        [StringLength(255)]
        public string Content { get; set; }

        [Required]
        [Range(0, 5)]
        [DisplayName("Stars")]
        public int StarsAmmount { get; set; }

        [Required]
        public int BookTemplateID { get; set; }

        [DisplayName("Book")]
        public BookDTO ReservedBook { get; set; }

        [Required]
        public int UserID { get; set; }

        public UserDTO User { get; set; }
    }
}
