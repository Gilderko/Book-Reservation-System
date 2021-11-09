using DAL.Entities.ConnectionTables;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.FullVersions
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
        public Language Language { get; set; }

        public ICollection<BookInstanceDTO> BookInstances { get; set; }

        public ICollection<ReviewDTO> Reviews { get; set; }

        // Many to many Relationships

        public ICollection<BookCollection_BookDTO> BookCollections { get; set; }

        public ICollection<Author_BookDTO> Authors { get; set; }

        public ICollection<Book_GenreDTO> Genres { get; set; }
    }
}
