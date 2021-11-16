using DAL.Entities.ConnectionTables;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Book : BaseEntity
    {
        [MaxLength(64)]
        public string Title { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string ISBN { get; set; }

        [Range(1, 10000)]
        public int PageCount { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DateOfRelease { get; set; }

        public Language Language { get; set; }

        public ICollection<BookInstance> BookInstances { get; set; }

        public ICollection<Review> Reviews { get; set; }

        // Many to many Relationships

        public ICollection<BookCollectionBook> BookCollections { get; set; }

        public ICollection<AuthorBook> Authors { get; set; }

        public ICollection<BookGenre> Genres { get; set; }
    }
}
