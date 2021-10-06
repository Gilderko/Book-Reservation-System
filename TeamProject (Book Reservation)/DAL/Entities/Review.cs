using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Review : BaseEntity
    {
        [Column(TypeName = "Date")]
        public DateTime CreationDate { get; set; }

        [MaxLength(255)]
        public string Content { get; set; }

        [Range(0, 5)]
        public int StarsAmmount { get; set; }

        public int BookTemplateID { get; set; }

        [ForeignKey(nameof(BookTemplateID))]
        public Book ReserveredBook { get; set; }

        public int UserID { get; set; }

        [ForeignKey(nameof(UserID))]
        public User User { get; set; }
    }
}
