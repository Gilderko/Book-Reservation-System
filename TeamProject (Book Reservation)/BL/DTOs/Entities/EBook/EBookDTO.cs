﻿using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Book;
using DAL.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Entities.EBook
{
    public class EBookDTO : BookDTO
    {
        [Required]
        public int MemorySize { get; set; }

        [Required]
        public EBookFormat Format { get; set; }

        public ICollection<EBookEReaderInstanceDTO> EReaders { get; set; }
    }
}