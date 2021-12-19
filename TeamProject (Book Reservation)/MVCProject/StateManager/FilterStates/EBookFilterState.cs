using BL.DTOs.Enums;

namespace MVCProject.StateManager.FilterStates
{
    public class EBookFilterState : BookFilterState
    {
        public EBookFilterState() : base()
        {
            Format = null;
        }

        public EBookFormatDTO? Format { get; set; }
    }
}
