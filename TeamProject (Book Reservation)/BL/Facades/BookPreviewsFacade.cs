using BL.Services;
using BL.DTOs.Entities.Book;
using DAL.Entities;

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
