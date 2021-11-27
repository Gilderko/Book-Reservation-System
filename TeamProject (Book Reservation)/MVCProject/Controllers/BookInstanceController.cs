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
using BL.DTOs.Entities.BookInstance;

namespace MVCProject.Controllers
{
    public class BookInstanceController : Controller
    {
        private readonly BookInstanceFacade _bookInstanceFacade;

        public BookInstanceController(BookInstanceFacade facade)
        {
            _bookInstanceFacade = facade;
        }

        // GET: BookInstance
        public async Task<IActionResult> Index()
        {            
            return View();
        }

        // GET: UserBookInstances
        public async Task<IActionResult> UserBookInstances()
        {
            int userId;
            if (User.Identity.Name is not null)
            {
                userId = int.Parse(User.Identity.Name);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View(await _bookInstanceFacade.GetBookInstancePrevsByUser(userId));
        }

        // GET: UserCreateBookInstance
        public async Task<IActionResult> UserCreateBookInstance()
        {
            return View();
        }

        // POST: BookInstance/UserCreateBookInstance
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCreateBookInstance([Bind("Conditon,BookTemplateID")] BookInstanceCreateDTO bookInstance)
        {
            int userId;
            if (User.Identity.Name is not null)
            {
                userId = int.Parse(User.Identity.Name);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                await _bookInstanceFacade.CreateUserBookInstance(userId, bookInstance);
                return RedirectToAction(nameof(Index));
            }
            return View(bookInstance);
        }

        // GET: BookInstance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var references = new string[]
            {
                nameof(BookInstanceDTO.Owner),
                nameof(BookInstanceDTO.FromBookTemplate)
            };

            var bookInstance = await _bookInstanceFacade.Get((int)id,references);
            if (bookInstance == null)
            {
                return NotFound();
            }

            return View(bookInstance);
        }

        [HttpPost]
        public async Task<IActionResult> Details(int? id, DateTime? StartDate, DateTime? EndDate)
        {
            if (id == null || StartDate == null || EndDate == null)
            {
                return NotFound();
            }

            var references = new string[]
            {
                nameof(BookInstanceDTO.Owner),
                nameof(BookInstanceDTO.FromBookTemplate)
            };

            var bookInstance = await _bookInstanceFacade.Get((int)id, references);
            var resultQuery = await _bookInstanceFacade.GetBookReservationPrevsByUser(id.Value, StartDate.Value, EndDate.Value);

            ViewData.Add("queryResult", resultQuery);

            if (bookInstance == null)
            {
                return NotFound();
            }

            return View(bookInstance);
        }

        // GET: BookInstance/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookInstance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Conditon,BookOwnerId,BookTemplateID,Id")] BookInstanceDTO bookInstance)
        {
            if (ModelState.IsValid)
            {
                await _bookInstanceFacade.Create(bookInstance);
                return RedirectToAction(nameof(Index));
            }
            return View(bookInstance);
        }

        // GET: BookInstance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookInstance = await _bookInstanceFacade.Get((int)id);
            if (bookInstance == null)
            {
                return NotFound();
            }
            return View(bookInstance);
        }

        // POST: BookInstance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Conditon,BookOwnerId,BookTemplateID,Id")] BookInstanceDTO bookInstance)
        {
            if (id != bookInstance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bookInstanceFacade.Update(bookInstance);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await BookInstanceExists(bookInstance.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bookInstance);
        }

        // GET: BookInstance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookInstance = await _bookInstanceFacade.Get((int)id);
            if (bookInstance == null)
            {
                return NotFound();
            }

            return View(bookInstance);
        }

        // POST: BookInstance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _bookInstanceFacade.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BookInstanceExists(int id)
        {
            var bookInstance = await _bookInstanceFacade.Get(id);
            return bookInstance != null;
        }
    }
}
