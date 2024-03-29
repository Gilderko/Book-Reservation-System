﻿using BL.DTOs.Entities.Review;
using BL.Facades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Config;
using MVCProject.StateManager;
using System;
using System.Threading.Tasks;

namespace MVCProject.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewFacade _facade;

        public ReviewController(ReviewFacade facade)
        {
            _facade = facade;
        }

        // GET: Review/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await GetWithReferences(id.Value);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        public IActionResult CreateReview(int? id)
        {
            if (id == null || !User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            StateKeeper.Instance.AddTillNextRequest(this, TempDataKeys.BookDTOId, id.Value);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReview(int? id, [Bind("Content,StarsAmmount,BookTemplateID,UserID")] ReviewDTO review)
        {
            StateKeeper.Instance.SaveSpecificObjectForNextRequest(this, TempDataKeys.BookDTOId);

            if (id == null || !User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            review.CreationDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                await _facade.Create(review);
                return RedirectToAction(nameof(Details), "Book", new { id = id.Value });
            }

            return View(review);
        }

        // GET: Review/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || (!User.IsInRole(GlobalConstants.UserRoleName) && !User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            var review = await GetWithReferences(id.Value);
            if (review == null || (review.UserID != int.Parse(User.Identity.Name) && !User.IsInRole(GlobalConstants.AdminRoleName)))
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
            if (id != review.Id || (!User.IsInRole(GlobalConstants.UserRoleName) && !User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            if ((review.UserID != int.Parse(User.Identity.Name) && !User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(review);
            }

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

            return RedirectToAction(nameof(Details), "Book", new { id = review.BookTemplateID });
        }

        // GET: Review/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || (!User.IsInRole(GlobalConstants.UserRoleName) && !User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            var review = await GetWithReferences(id.Value);
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
            if (!User.IsInRole(GlobalConstants.UserRoleName) && !User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            var review = await _facade.Get(id);
            if (review == null || (int.Parse(User.Identity.Name) != review.UserID && !User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            _facade.Delete(id);
            return RedirectToAction(nameof(Details), "Book", new { id = review.BookTemplateID });
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
