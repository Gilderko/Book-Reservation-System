using Autofac.Features.OwnedInstances;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Author;
using BL.DTOs.Entities.Book;
using BL.Services;
using DAL.Entities;
using DAL.Entities.ConnectionTables;
using Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task Create(AuthorDTO author)
        {
            await _authorService.Insert(author);
            _unitOfWork.Commit();
        }

        public async Task<AuthorDTO> Get(int id, string[] refsToLoad = null, string[] collectToLoad = null)
        {
            var result = await _authorService.GetByID(id, refsToLoad, collectToLoad);
            return result;
        }

        public void Update(AuthorDTO author)
        {
            _authorService.Update(author);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _authorService.DeleteById(id);
            _unitOfWork.Commit();
        }

        public async Task AddBookToAuthor(AuthorDTO author, BookDTO book)
        {
            await _authorBookService.Insert(new AuthorBookDTO
            {                
                Author = author,
                AuthorID = author.Id,


                Book = book,
                BookID = book.Id
            });
            _unitOfWork.Commit();
        }
    }
}
