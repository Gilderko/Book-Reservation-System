using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.ConnectionTables
{
    public class AddBookInCollectionDTO : IEntityDTO
    {
        [Required]
        public int BookCollectionID { get; set; }

        public int BookID { get; set; }
    }
}
