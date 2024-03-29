﻿using BL.DTOs.Entities.Review;
using BL.Services;
using DAL.Entities;
using Infrastructure;
using System;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class ReviewFacade
    {
        private IUnitOfWork _unitOfWork;
        private ICRUDService<ReviewDTO, Review> _service;

        public ReviewFacade(IUnitOfWork unitOfWork, ICRUDService<ReviewDTO, Review> service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public async Task Create(ReviewDTO review)
        {
            await _service.Insert(review);
            _unitOfWork.Commit();
        }

        public async Task<ReviewDTO> Get(int id, string[] refsToLoad = null, string[] collectToLoad = null)
        {
            return await _service.GetByID(id, refsToLoad, collectToLoad);
        }

        public void Update(ReviewDTO review)
        {
            _service.Update(review);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _service.DeleteById(id);
            _unitOfWork.Commit();
        }

        public void AddReview(int bookId, ReviewDTO review)
        {
            review.CreationDate = DateTime.Now;
            review.BookTemplateID = bookId;
            _unitOfWork.Commit();
        }
    }
}
