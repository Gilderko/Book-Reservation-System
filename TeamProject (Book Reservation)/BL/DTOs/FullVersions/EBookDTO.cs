﻿using BL.DTOs.ConnectionTables;
using DAL.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.FullVersions
{
    public class EBookDTO : BookDTO
    {
        [Required]
        public int MemorySize { get; set; }

        [Required]
        public EBookFormat Format { get; set; }

        public ICollection<EBook_EReaderInstanceDTO> EReaders { get; set; }
    }
}
