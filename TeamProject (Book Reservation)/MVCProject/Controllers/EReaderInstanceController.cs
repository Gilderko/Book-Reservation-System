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
using BL.DTOs.Entities.EReaderInstance;

namespace MVCProject.Controllers
{
    public class EReaderInstanceController : Controller
    {
        private readonly EReaderInstanceFacade _facade;

        public EReaderInstanceController(EReaderInstanceFacade facade)
        {
            _facade = facade;
        }

        // GET: EReaderInstance
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: EReaderInstance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eReaderInstance = await _facade.Get((int)id);
            if (eReaderInstance == null)
            {
                return NotFound();
            }

            return View(eReaderInstance);
        }

        // GET: EReaderInstance/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EReaderInstance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,EreaderOwnerId,EReaderTemplateID,Id")] EReaderInstanceDTO eReaderInstance)
        {
            if (ModelState.IsValid)
            {
                await _facade.Create(eReaderInstance);
                return RedirectToAction(nameof(Index));
            }
            return View(eReaderInstance);
        }

        // GET: EReaderInstance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eReaderInstance = await _facade.Get((int)id);
            if (eReaderInstance == null)
            {
                return NotFound();
            }
            return View(eReaderInstance);
        }

        // POST: EReaderInstance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Description,EreaderOwnerId,EReaderTemplateID,Id")] EReaderInstanceDTO eReaderInstance)
        {
            if (id != eReaderInstance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _facade.Update(eReaderInstance);
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
            return View(eReaderInstance);
        }

        // GET: EReaderInstance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eReaderInstance = await _facade.Get((int)id);
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
            await _facade.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool EReaderInstanceExists(int id)
        {
            return _facade.Get(id) != null;
        }
    }
}
