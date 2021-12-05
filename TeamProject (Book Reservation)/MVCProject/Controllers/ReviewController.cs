﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Entities;
using BL.Facades;
using BL.DTOs.Entities.Review;
using Microsoft.AspNetCore.Authorization;
using MVCProject.Config;

namespace MVCProject.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewFacade _facade;

        public ReviewController(ReviewFacade facade)
        {
            _facade = facade;
        }

        // GET: Review
        public IActionResult Index()
        {
            return NotFound();
        }

        // GET: Review/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await GetWithReferences((int)id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Review/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Review/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreationDate,Content,StarsAmmount,BookTemplateID,UserID,Id")] ReviewDTO review)
        {
            if (ModelState.IsValid)
            {
                await _facade.Create(review);
                return RedirectToAction(nameof(Details), nameof(Book), new { id = review.BookTemplateID });
            }
            return View(review);
        }

        public IActionResult CreateReview(int? bookId)
        {
            if (bookId == null || !User.Identity.IsAuthenticated) 
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReview(int? bookId, [Bind("CreationDate,Content,StarsAmmount,BookTemplateID,UserID,Id")] ReviewDTO review)
        {
            if (bookId == null || User.Identity.Name == null)
            {
                return NotFound();
            }

            int userId = int.Parse(User.Identity.Name);

            review.CreationDate = DateTime.Now;
            review.UserID = userId;
            review.BookTemplateID = (int)bookId;

            if (ModelState.IsValid)
            {
                await _facade.Create(review);
                return RedirectToAction(nameof(Details), nameof(Book), new { id = bookId });
            }

            return View(review);
        }

        // GET: Review/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || User.Identity.Name == null)
            {
                return NotFound();
            }

            var review = await GetWithReferences((int)id);
            if (review == null || int.Parse(User.Identity.Name) != review.UserID)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: Review/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CreationDate,Content,StarsAmmount,BookTemplateID,UserID,Id")] ReviewDTO review)
        {
            if (id != review.Id || User.Identity.Name == null || int.Parse(User.Identity.Name) != review.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _facade.Update(review);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ReviewExists(review.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), nameof(Book), new { id = review.BookTemplateID });
            }
            return View(review);
        }

        // GET: Review/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || User.Identity.Name == null)
            {
                return NotFound();
            }
            
            var review = await GetWithReferences((int) id);
            if (review == null || (int.Parse(User.Identity.Name) != review.UserID && !User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.Name == null)
            {
                return NotFound();
            }

            var review = await _facade.Get(id);
            if (review == null || (int.Parse(User.Identity.Name) != review.UserID && !User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            _facade.Delete(id);
            return RedirectToAction(nameof(Details), nameof(Book), new { id = id });
        }

        private async Task<bool> ReviewExists(int id)
        {
            var review = await _facade.Get(id);
            return review != null;
        }

        private async Task<ReviewDTO> GetWithReferences(int id)
        {
            string[] referencesToLoad = new[]
            {
                nameof(ReviewDTO.User),
                nameof(ReviewDTO.ReservedBook)
            };

            var review = await _facade.Get(id, referencesToLoad);
            return review;
        }
    }
}
