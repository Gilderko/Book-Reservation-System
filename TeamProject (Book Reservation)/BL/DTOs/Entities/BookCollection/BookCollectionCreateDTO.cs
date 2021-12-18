using System;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.BookCollection
{
    public class BookCollectionCreateDTO : BaseEntityDTO
    {
        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }
    }
}
