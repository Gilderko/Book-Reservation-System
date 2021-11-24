using System;
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
        private readonly BookInstanceFacade _facade;

        public BookInstanceController(BookInstanceFacade facade)
        {
            _facade = facade;
        }

        // GET: BookInstance
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: BookInstance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookInstance = await _facade.Get((int)id);
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
                await _facade.Create(bookInstance);
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

            var bookInstance = await _facade.Get((int)id);
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
                    await _facade.Update(bookInstance);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookInstanceExists(bookInstance.Id))
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

            var bookInstance = await _facade.Get((int)id);
            if (bookInstance == null)
            {
                return NotFound();
            }

            return View(bookInstance);
        }

        // POST: BookInstance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _facade.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool BookInstanceExists(int id)
        {
            return _facade.Get(id) != null;
        }
    }
}
