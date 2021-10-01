using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class BookTemplate : BaseEntity
    {
        [MaxLength(64)]
        public string Title { get; set; }

        [MaxLength(512)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string ISBN { get; set; }
        
        [Range(1, 10000)]
        public int PageCount { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DateOfRelease { get; set; }

        public Language Language { get; set; }    

        public ICollection<Author> Authors { get; set; }

        public ICollection<Genre> Genres { get; set; }

        public ICollection<BookInstance> BookInstances { get; set; }

        public ICollection<BookCollection> BookCollection { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
