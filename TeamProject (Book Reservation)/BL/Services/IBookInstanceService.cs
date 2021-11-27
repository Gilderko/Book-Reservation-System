using BL.DTOs.Entities.BookInstance;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IBookInstanceService : ICRUDService<BookInstanceDTO, BookInstance>
    {
        public Task CreateBookInstance(int ownerId, BookInstanceCreateDTO createBookInstance);
    }
}
