using BL.DTOs.FullVersions;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.Previews
{
    public class BookInstancePrevDTO : BaseEntityDTO
    {
        public BookInstanceCondition Conditon { get; set; }

        public UserDTO Owner { get; set; }

        public BookDTO FromBookTemplate { get; set; }
    }
}
