using System;
using BL.DTOs.Entities.Review;
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
            _unitOfWork.Commit();
        }

        public ReviewDTO Get(int id)
        {
            return _service.GetByID(id);
        }

        public void Update(ReviewDTO review)
        {
            _service.Update(review);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _service.Delete(id);
            _unitOfWork.Commit();
        }

        public void AddReview(int bookId, ReviewDTO review)
        {
            review.CreationDate = DateTime.Now;
            review.BookTemplateID = bookId;
            _unitOfWork.Commit();
        }
        
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
