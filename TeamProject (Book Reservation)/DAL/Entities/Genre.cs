using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{

    public class Genre
    {
        [Key]
        public GenreType GenreID { get; set; }

        public ICollection<BookTemplate> Books { get; set; }
    }

    /// <summary>
    /// Used as keys into the database for the class Genre
    /// </summary>
    public enum GenreType
    {
        Scifi, Fantasy, Detective, Medieval, Lovestory, Historical, Classic
    }
}
