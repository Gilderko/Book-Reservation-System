using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Entities;

namespace MVCProject.Controllers
{
    public class BookCollectionController : Controller
    {
        private readonly BookRentalDbContext _context;

        public BookCollectionController(BookRentalDbContext context)
        {
            _context = context;
        }

        // GET: BookCollection
        public async Task<IActionResult> Index()
        {
            var bookRentalDbContext = _context.BookCollections.Include(b => b.OwnerUser);
            return View(await bookRentalDbContext.ToListAsync());
        }

        // GET: BookCollection/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCollection = await _context.BookCollections
                .Include(b => b.OwnerUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookCollection == null)
            {
                return NotFound();
            }

            return View(bookCollection);
        }

        // GET: BookCollection/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: BookCollection/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,CreationDate,UserId,Id")] BookCollection bookCollection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookCollection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bookCollection.UserId);
            return View(bookCollection);
        }

        // GET: BookCollection/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCollection = await _context.BookCollections.FindAsync(id);
            if (bookCollection == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bookCollection.UserId);
            return View(bookCollection);
        }

        // POST: BookCollection/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,CreationDate,UserId,Id")] BookCollection bookCollection)
        {
            if (id != bookCollection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookCollection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookCollectionExists(bookCollection.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bookCollection.UserId);
            return View(bookCollection);
        }

        // GET: BookCollection/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCollection = await _context.BookCollections
                .Include(b => b.OwnerUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookCollection == null)
            {
                return NotFound();
            }

            return View(bookCollection);
        }

        // POST: BookCollection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookCollection = await _context.BookCollections.FindAsync(id);
            _context.BookCollections.Remove(bookCollection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookCollectionExists(int id)
        {
            return _context.BookCollections.Any(e => e.Id == id);
        }
    }
}
