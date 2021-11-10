using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class User : BaseEntity
    {
        [MaxLength(32)]
        public string Name { get; set; }

        [MaxLength(32)]
        public string Surname { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(256)]
        public string HashedPassword { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<BookCollection> BookCollections { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<EBookInstance> MyBooks { get; set; }

        public ICollection<EReaderInstance> MyEReaders { get; set; }
    }
}
