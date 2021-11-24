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
using BL.DTOs.Entities.BookCollection;

namespace MVCProject.Controllers
{
    public class BookCollectionController : Controller
    {
        private readonly BookCollectionFacade _facade;

        public BookCollectionController(BookCollectionFacade facade)
        {
            _facade = facade;
        }

        // GET: BookCollection
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: BookCollection/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCollection = await _facade.Get((int)id);
            if (bookCollection == null)
            {
                return NotFound();
            }

            return View(bookCollection);
        }

        // GET: BookCollection/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookCollection/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,CreationDate,UserId,Id")] BookCollectionDTO bookCollection)
        {
            if (ModelState.IsValid)
            {
                await _facade.Create(bookCollection);
                return RedirectToAction(nameof(Index));
            }
            return View(bookCollection);
        }

        // GET: BookCollection/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCollection = await _facade.Get((int)id);
            if (bookCollection == null)
            {
                return NotFound();
            }
            return View(bookCollection);
        }

        // POST: BookCollection/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,CreationDate,UserId,Id")] BookCollectionDTO bookCollection)
        {
            if (id != bookCollection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _facade.Update(bookCollection);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await BookCollectionExists(bookCollection.Id))
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
            return View(bookCollection);
        }

        // GET: BookCollection/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCollection = await _facade.Get((int)id);
            if (bookCollection == null)
            {
                return NotFound();
            }

            return View(bookCollection);
        }

        // POST: BookCollection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _facade.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BookCollectionExists(int id)
        {
            var bookCollection = await _facade.Get(id);
            return bookCollection != null;
        }
    }
}
