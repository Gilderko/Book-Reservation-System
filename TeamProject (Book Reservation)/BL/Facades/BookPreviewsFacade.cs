using BL.DTOs.Previews;
using BL.Services;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class BookPreviewsFacade
    {
        private BookPreviewService _service;

        public BookPreviewsFacade(BookPreviewService service)
        {
            _service = service;
        }

        //public IEnumerable<BookPrevDTO> GetBookPreviews()
        //{
        //
        //}
    }
}
