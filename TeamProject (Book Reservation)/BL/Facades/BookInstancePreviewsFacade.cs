using BL.DTOs.FullVersions;
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
    public class BookInstancePreviewsFacade
    {
        private BookInstancePreviewService _service;

        public BookInstancePreviewsFacade(BookInstancePreviewService service)
        {
            _service = service;
        }

        public IEnumerable<BookInstancePrevDTO> GetBookInstancesByUser(UserDTO user, int pageNumber, int pageSize)
        {
            return _service.GetBookInstancesByUser(user, pageNumber, pageSize);
        }
    }
}
