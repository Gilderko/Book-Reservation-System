namespace MVCProject.StateManager.FilterStates
{
    public class EReaderInstanceFilterState : FilterState
    {
        public EReaderInstanceFilterState()
        {
            Description = null;
            Company = null;
            Model = null;
            MemoryFrom = null;
            MemoryTo = null;
        }

        public string Description { get; set; }
        public string Company { get; set; }
        public string Model { get; set; }
        public int? MemoryFrom { get; set; }
        public int? MemoryTo { get; set; }
    }
}
