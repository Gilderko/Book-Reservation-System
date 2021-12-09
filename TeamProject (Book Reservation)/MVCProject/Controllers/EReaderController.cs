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
using BL.DTOs.Entities.EReader;
using Autofac;

namespace MVCProject.Controllers
{
    public class EReaderController : Controller
    {
        private readonly EReaderFacade _facade;

        public EReaderController(EReaderFacade facade)
        {
            _facade = facade;
        }

        // GET: EReader
        public async Task<IActionResult> Index()
        {
            /*return View(await _context.EReaderTemplates.ToListAsync());*/
            return View();
        }

        // GET: EReader/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eReader = await _facade.Get((int)id);
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
        public async Task<IActionResult> Create([Bind("Model,CompanyMake,MemoryInMB,Id")] EReaderDTO eReader)
        {
            if (ModelState.IsValid)
            {
                await _facade.Create(eReader);
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

            var eReader = await _facade.Get((int)id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Model,CompanyMake,MemoryInMB,Id")] EReaderDTO eReader)
        {
            if (id != eReader.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _facade.Update(eReader);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await EReaderExists(eReader.Id))
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

            var eReader = await _facade.Get((int)id);
            if (eReader == null)
            {
                return NotFound();
            }

            return View(eReader);
        }

        // POST: EReader/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _facade.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EReaderExists(int id)
        {
            var eReader = await _facade.Get(id);
            return eReader != null;
        }
    }
}
