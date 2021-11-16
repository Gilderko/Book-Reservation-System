using BL.Services;
using BL.DTOs.Entities.Book;
using DAL.Entities;

namespace BL.Facades
{
    public class BookPreviewsFacade
    {
        private BookPreviewService<BookPrevDTO, Book> _service;

        public BookPreviewsFacade(BookPreviewService<BookPrevDTO, Book> service)
        {
            _service = service;
        }

        //public IEnumerable<BookPrevDTO> GetBookPreviews()
        //{
        //
        //}
    }
}
