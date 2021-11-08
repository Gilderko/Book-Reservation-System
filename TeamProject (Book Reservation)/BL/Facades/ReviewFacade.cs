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
    public class ReviewFacade
    {
        private IUnitOfWork unitOfWork;
        private CRUDService<ReviewDTO, Review> service;

        public ReviewFacade()
        {
            unitOfWork = new UnitOfWork(new BookRentalDbContext());
            service = new CRUDService<ReviewDTO, Review>(unitOfWork);
        }

        public void Create(ReviewDTO review)
        {
            service.Insert(review);
        }

        public ReviewDTO Get(int id)
        {
            return service.GetByID(id);
        }

        public void Update(ReviewDTO review)
        {
            service.Update(review);
        }

        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
