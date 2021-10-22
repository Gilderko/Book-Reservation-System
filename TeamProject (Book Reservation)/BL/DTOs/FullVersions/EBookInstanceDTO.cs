namespace BL.DTOs.FullVersions
{
    public class EBookInstanceDTO : BaseEntityDTO
    {
        public int EBookTemplateID { get; set; }

        public EBookDTO FromBookTemplate { get; set; }

        public int EReaderID { get; set; }

        public EReaderInstanceDTO EReaderPlace { get; set; }
    }
}
