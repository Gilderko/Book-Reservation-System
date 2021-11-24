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
    public class EReaderController : Controller
    {
        private readonly BookRentalDbContext _context;

        public EReaderController(BookRentalDbContext context)
        {
            _context = context;
        }

        // GET: EReader
        public async Task<IActionResult> Index()
        {
            return View(await _context.EReaderTemplates.ToListAsync());
        }

        // GET: EReader/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eReader = await _context.EReaderTemplates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eReader == null)
            {
                return NotFound();
            }

            return View(eReader);
        }

        // GET: EReader/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EReader/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Model,CompanyMake,MemoryInMB,Id")] EReader eReader)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eReader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eReader);
        }

        // GET: EReader/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eReader = await _context.EReaderTemplates.FindAsync(id);
            if (eReader == null)
            {
                return NotFound();
            }
            return View(eReader);
        }

        // POST: EReader/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Model,CompanyMake,MemoryInMB,Id")] EReader eReader)
        {
            if (id != eReader.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eReader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EReaderExists(eReader.Id))
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
            return View(eReader);
        }

        // GET: EReader/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eReader = await _context.EReaderTemplates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eReader == null)
            {
                return NotFound();
            }

            return View(eReader);
        }

        // POST: EReader/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eReader = await _context.EReaderTemplates.FindAsync(id);
            _context.EReaderTemplates.Remove(eReader);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EReaderExists(int id)
        {
            return _context.EReaderTemplates.Any(e => e.Id == id);
        }
    }
}
