using BL.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
