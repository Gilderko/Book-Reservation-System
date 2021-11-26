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
        private readonly BookCollectionFacade _bookCollectionFacade;

        public BookCollectionController(BookCollectionFacade facade)
        {
            _bookCollectionFacade = facade;
        }

        // GET: BookCollection
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: BookCollection/UserCollections
        public async Task<IActionResult> UserCollections()
        {
            int id;

            if (User.Identity.Name is not null)
            {
                id = int.Parse(User.Identity.Name);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View(await _bookCollectionFacade.GetBookCollectionPrevsByUser(id));
        }

        // GET: BookCollection/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCollection = await _bookCollectionFacade.Get((int)id);
            if (bookCollection == null)
            {
                return NotFound();
            }

            return View(bookCollection);
        }

        // GET: BookCollection/UserCreateCollection
        public IActionResult UserCreateCollection()
        {
            return View();
        }

        // POST: BookCollection/UserCreateCollection
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCreateCollection([Bind("Title,Description")] BookCollectionCreateDTO bookCollection)
        {
            if (ModelState.IsValid)
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

                await _bookCollectionFacade.CreateUserCollection(bookCollection, userId);
                return RedirectToAction(nameof(UserCollections));
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
                await _bookCollectionFacade.Create(bookCollection);
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

            var bookCollection = await _bookCollectionFacade.Get((int)id);
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
                    _bookCollectionFacade.Update(bookCollection);
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

            var bookCollection = await _bookCollectionFacade.Get((int)id);
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
            _bookCollectionFacade.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BookCollectionExists(int id)
        {
            var bookCollection = await _bookCollectionFacade.Get(id);
            return bookCollection != null;
        }
    }
}
