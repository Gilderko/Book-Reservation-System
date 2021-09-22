﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class EBookInstance : BaseEntity
    {
        public int EBookTemplateID { get; set; }

        [ForeignKey(nameof(EBookTemplateID))]
        public EBookTemplate FromBookTemplate { get; set; }

        public int EReaderID { get; set; }

        [ForeignKey(nameof(EReaderID))]
        public EReader EReaderPlace { get; set; }
    }
}
