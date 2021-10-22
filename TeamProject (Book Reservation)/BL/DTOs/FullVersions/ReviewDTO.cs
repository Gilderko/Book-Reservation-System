using System;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.FullVersions
{
    public class ReviewDTO : BaseEntityDTO
    {
        public DateTime CreationDate { get; set; }

        [StringLength(255)]
        public string Content { get; set; }

        [Range(0, 5)]
        public int StarsAmmount { get; set; }

        public int BookTemplateID { get; set; }

        public BookDTO ReserveredBook { get; set; }

        public int UserID { get; set; }

        public UserDTO User { get; set; }
    }
}
