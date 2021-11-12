using BL.DTOs.FullVersions;
using BL.Services;
using DAL.Entities;
using Infrastructure;

namespace BL.Facades
{
    public class ReviewFacade
    {
        private IUnitOfWork _unitOfWork;
        private CRUDService<ReviewDTO, Review> _service;

        public ReviewFacade(IUnitOfWork unitOfWork, CRUDService<ReviewDTO, Review> service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public void Create(ReviewDTO review)
        {
            _service.Insert(review);
        }

        public ReviewDTO Get(int id)
        {
            return _service.GetByID(id);
        }

        public void Update(ReviewDTO review)
        {
            _service.Update(review);
        }

        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
