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
    public class BookCollectionPreviewsFacade
    {
        private BookCollectionPreviewService _service;

        public BookCollectionPreviewsFacade(BookCollectionPreviewService service)
        {
            _service = service;
        }

        public IEnumerable<BookCollectionPrevDTO> GetBookCollectionsByUser(UserDTO user, int pageNumber, int pageSize)
        {
            return _service.GetBookCollectionsByUser(user, pageNumber, pageSize);
        }
    }
}
