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
using BL.DTOs.Entities.EBook;

namespace MVCProject.Controllers
{
    public class EBookController : Controller
    {
        private readonly EBookFacade _facade;

        public EBookController(EBookFacade facade)
        {
            _facade = facade;
        }

        // GET: EBook
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: EBook/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eBook = await _facade.Get((int)id);
            if (eBook == null)
            {
                return NotFound();
            }

            return View(eBook);
        }

        // GET: EBook/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EBook/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemorySize,Format,Title,Description,ISBN,PageCount,DateOfRelease,Language,Id")] EBookDTO eBook)
        {
            if (ModelState.IsValid)
            {
                await _facade.Create(eBook);
                return RedirectToAction(nameof(Index));
            }
            return View(eBook);
        }

        // GET: EBook/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eBook = await _facade.Get((int)id);
            if (eBook == null)
            {
                return NotFound();
            }
            return View(eBook);
        }

        // POST: EBook/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemorySize,Format,Title,Description,ISBN,PageCount,DateOfRelease,Language,Id")] EBookDTO eBook)
        {
            if (id != eBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _facade.Update(eBook);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await EBookExists(eBook.Id))
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
            return View(eBook);
        }

        // GET: EBook/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eBook = await _facade.Get((int)id);
            if (eBook == null)
            {
                return NotFound();
            }

            return View(eBook);
        }

        // POST: EBook/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _facade.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EBookExists(int id)
        {
            var eBook = await _facade.Get(id);
            return eBook != null;
        }
    }
}
