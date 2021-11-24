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
    public class BookInstanceController : Controller
    {
        private readonly BookRentalDbContext _context;

        public BookInstanceController(BookRentalDbContext context)
        {
            _context = context;
        }

        // GET: BookInstance
        public async Task<IActionResult> Index()
        {
            var bookRentalDbContext = _context.BookInstances.Include(b => b.FromBookTemplate).Include(b => b.Owner);
            return View(await bookRentalDbContext.ToListAsync());
        }

        // GET: BookInstance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookInstance = await _context.BookInstances
                .Include(b => b.FromBookTemplate)
                .Include(b => b.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookInstance == null)
            {
                return NotFound();
            }

            return View(bookInstance);
        }

        // GET: BookInstance/Create
        public IActionResult Create()
        {
            ViewData["BookTemplateID"] = new SelectList(_context.Books, "Id", "Discriminator");
            ViewData["BookOwnerId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: BookInstance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Conditon,BookOwnerId,BookTemplateID,Id")] BookInstance bookInstance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookInstance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookTemplateID"] = new SelectList(_context.Books, "Id", "Discriminator", bookInstance.BookTemplateID);
            ViewData["BookOwnerId"] = new SelectList(_context.Users, "Id", "Id", bookInstance.BookOwnerId);
            return View(bookInstance);
        }

        // GET: BookInstance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookInstance = await _context.BookInstances.FindAsync(id);
            if (bookInstance == null)
            {
                return NotFound();
            }
            ViewData["BookTemplateID"] = new SelectList(_context.Books, "Id", "Discriminator", bookInstance.BookTemplateID);
            ViewData["BookOwnerId"] = new SelectList(_context.Users, "Id", "Id", bookInstance.BookOwnerId);
            return View(bookInstance);
        }

        // POST: BookInstance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Conditon,BookOwnerId,BookTemplateID,Id")] BookInstance bookInstance)
        {
            if (id != bookInstance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookInstance);
                    await _context.SaveChangesAsync();
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
            ViewData["BookTemplateID"] = new SelectList(_context.Books, "Id", "Discriminator", bookInstance.BookTemplateID);
            ViewData["BookOwnerId"] = new SelectList(_context.Users, "Id", "Id", bookInstance.BookOwnerId);
            return View(bookInstance);
        }

        // GET: BookInstance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookInstance = await _context.BookInstances
                .Include(b => b.FromBookTemplate)
                .Include(b => b.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var bookInstance = await _context.BookInstances.FindAsync(id);
            _context.BookInstances.Remove(bookInstance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookInstanceExists(int id)
        {
            return _context.BookInstances.Any(e => e.Id == id);
        }
    }
}
