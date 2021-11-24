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
    public class EReaderInstanceController : Controller
    {
        private readonly BookRentalDbContext _context;

        public EReaderInstanceController(BookRentalDbContext context)
        {
            _context = context;
        }

        // GET: EReaderInstance
        public async Task<IActionResult> Index()
        {
            var bookRentalDbContext = _context.EReaderInstances.Include(e => e.EReaderTemplate).Include(e => e.Owner);
            return View(await bookRentalDbContext.ToListAsync());
        }

        // GET: EReaderInstance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eReaderInstance = await _context.EReaderInstances
                .Include(e => e.EReaderTemplate)
                .Include(e => e.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eReaderInstance == null)
            {
                return NotFound();
            }

            return View(eReaderInstance);
        }

        // GET: EReaderInstance/Create
        public IActionResult Create()
        {
            ViewData["EReaderTemplateID"] = new SelectList(_context.EReaderTemplates, "Id", "Id");
            ViewData["EreaderOwnerId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: EReaderInstance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,EreaderOwnerId,EReaderTemplateID,Id")] EReaderInstance eReaderInstance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eReaderInstance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EReaderTemplateID"] = new SelectList(_context.EReaderTemplates, "Id", "Id", eReaderInstance.EReaderTemplateID);
            ViewData["EreaderOwnerId"] = new SelectList(_context.Users, "Id", "Id", eReaderInstance.EreaderOwnerId);
            return View(eReaderInstance);
        }

        // GET: EReaderInstance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eReaderInstance = await _context.EReaderInstances.FindAsync(id);
            if (eReaderInstance == null)
            {
                return NotFound();
            }
            ViewData["EReaderTemplateID"] = new SelectList(_context.EReaderTemplates, "Id", "Id", eReaderInstance.EReaderTemplateID);
            ViewData["EreaderOwnerId"] = new SelectList(_context.Users, "Id", "Id", eReaderInstance.EreaderOwnerId);
            return View(eReaderInstance);
        }

        // POST: EReaderInstance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Description,EreaderOwnerId,EReaderTemplateID,Id")] EReaderInstance eReaderInstance)
        {
            if (id != eReaderInstance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eReaderInstance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EReaderInstanceExists(eReaderInstance.Id))
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
            ViewData["EReaderTemplateID"] = new SelectList(_context.EReaderTemplates, "Id", "Id", eReaderInstance.EReaderTemplateID);
            ViewData["EreaderOwnerId"] = new SelectList(_context.Users, "Id", "Id", eReaderInstance.EreaderOwnerId);
            return View(eReaderInstance);
        }

        // GET: EReaderInstance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eReaderInstance = await _context.EReaderInstances
                .Include(e => e.EReaderTemplate)
                .Include(e => e.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eReaderInstance == null)
            {
                return NotFound();
            }

            return View(eReaderInstance);
        }

        // POST: EReaderInstance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eReaderInstance = await _context.EReaderInstances.FindAsync(id);
            _context.EReaderInstances.Remove(eReaderInstance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EReaderInstanceExists(int id)
        {
            return _context.EReaderInstances.Any(e => e.Id == id);
        }
    }
}
