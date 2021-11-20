using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Author;
using BL.DTOs.Entities.Book;
using BL.Services;
using DAL.Entities;
using DAL.Entities.ConnectionTables;
using Infrastructure;

namespace BL.Facades
{
    public class AuthorFacade
    {
        private IUnitOfWork _unitOfWork;
        private ICRUDService<AuthorDTO, Author> _authorService;
        private ICRUDService<AuthorBookDTO, AuthorBook> _authorBookService;

        public AuthorFacade(IUnitOfWork unitOfWork,
                            ICRUDService<AuthorDTO, Author> authorService,
                            ICRUDService<AuthorBookDTO, AuthorBook> authorBookService)
        {
            _unitOfWork = unitOfWork;
            _authorService = authorService;
            _authorBookService = authorBookService;
        }

        public void Create(AuthorDTO author)
        {
            _authorService.Insert(author);
            _unitOfWork.Commit();
        }

        public AuthorDTO Get(int id)
        {
            return _authorService.GetByID(id);
        }

        public void Update(AuthorDTO author)
        {
            _authorService.Update(author);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _authorService.Delete(id);
            _unitOfWork.Commit();
        }

        public void AddBookToAuthor(AuthorDTO author, BookDTO book)
        {
            _authorBookService.Insert(new AuthorBookDTO { Author = author, Book = book });
            _unitOfWork.Commit();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
