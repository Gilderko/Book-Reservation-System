using System;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.FullVersions
{
    public class ReviewDTO : BaseEntityDTO
    {
        [Required]
        public DateTime CreationDate { get; set; }

        [StringLength(255)]
        public string Content { get; set; }

        [Required]
        [Range(0, 5)]
        public int StarsAmmount { get; set; }

        [Required]
        public int BookTemplateID { get; set; }

        public BookDTO ReserveredBook { get; set; }

        [Required]
        public int UserID { get; set; }

        public UserDTO User { get; set; }
    }
}
