using BL.DTOs.FullVersions;
using BL.Services;
using DAL;
using DAL.Entities;
using EFInfrastructure;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class AuthorFacade
    {
        private IUnitOfWork unitOfWork;
        private CRUDService<AuthorDTO, Author> service;

        public AuthorFacade()
        {
            unitOfWork = new UnitOfWork(new BookRentalDbContext());
            service = new CRUDService<AuthorDTO, Author>(unitOfWork);
        }
        
        public void Create(AuthorDTO author)
        {
            service.Insert(author);
        }

        public AuthorDTO Get(int id)
        {
            return service.GetByID(id);
        }

        public void Update(AuthorDTO author)
        {
            service.Update(author);
        }

        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
