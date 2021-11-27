using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.BookInstance;
using BL.DTOs.Entities.Review;
using BL.DTOs.Enums;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.Book
{
    public class BookDTO : BaseEntityDTO
    {
        [Required]
        [StringLength(64)]
        public string Title { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        public string ISBN { get; set; }

        [Required]
        [Range(1, 10000)]
        public int PageCount { get; set; }

        [Required]
        public DateTime DateOfRelease { get; set; }

        [Required]
        public LanguageDTO Language { get; set; }

        public ICollection<BookInstanceDTO> BookInstances { get; set; }

        public ICollection<ReviewDTO> Reviews { get; set; }

        // Many to many Relationships

        public ICollection<BookCollectionBookDTO> BookCollections { get; set; }

        public ICollection<AuthorBookDTO> Authors { get; set; }

        public ICollection<BookGenreDTO> Genres { get; set; }
    }
}
