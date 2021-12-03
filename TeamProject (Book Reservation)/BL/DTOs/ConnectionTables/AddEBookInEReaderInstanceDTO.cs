using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.ConnectionTables
{
    public class AddEBookInEReaderInstanceDTO : IEntityDTO
    {
        public int EBookID { get; set; }

        [Required]
        public int EReaderInstanceID { get; set; }
    }
}
