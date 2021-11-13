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
        private IUnitOfWork _unitOfWork;
        private BookPreviewService _service;

        public BookPreviewsFacade(IUnitOfWork unitOfWork, BookPreviewService service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public IEnumerable<BookPrevDTO> GetBookPreviews()
        {

        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
