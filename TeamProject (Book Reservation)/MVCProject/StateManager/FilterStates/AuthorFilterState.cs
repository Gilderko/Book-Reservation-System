using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.StateManager.FilterStates
{
    public class AuthorFilterState : FilterState
    {
        public AuthorFilterState()
        {
            Name = null;
            Surname = null;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
