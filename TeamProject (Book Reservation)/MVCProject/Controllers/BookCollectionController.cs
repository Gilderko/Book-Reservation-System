using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BL.Facades;
using BL.DTOs.Entities.BookCollection;
using BL.DTOs.ConnectionTables;
using MVCProject.Config;

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
            int userId;

            if (User.Identity.Name is not null)
            {
                userId = int.Parse(User.Identity.Name);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View(await _bookCollectionFacade.GetBookCollectionPrevsByUser(userId));
        }


        // GET: BookCollection/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCollection = await _bookCollectionFacade.GetBookCollectionWithBooksAndOwner((int)id);
            if (bookCollection == null)
            {
                return NotFound();
            }

            ViewData["books"] = await _bookCollectionFacade.GetBookPreviewsWithAuthorsForCollection(bookCollection);

            return View(bookCollection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(int bookCollectionId, int bookToDeleteId)
        {
            _bookCollectionFacade.DeleteBookFromCollection(bookCollectionId,bookToDeleteId);           

            return RedirectToAction("Details",new { id = bookCollectionId });
        }

        // GET: BookCollection/UserCreateCollection
        [HttpGet]
        public IActionResult UserCreateCollection(int? id)
        {
            return View();
        }

        // GET: BookCollection/Create
        public IActionResult UserAddBookInCollection()
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

            var collectionPrevs = _bookCollectionFacade.GetBookCollectionPrevsByUser(userId).Result;

            ViewData["collections"] = collectionPrevs;
            return View();
        }

        // POST: BookCollection/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserAddBookInCollection(int? id, [Bind("BookCollectionID")] AddBookInCollectionDTO bookCollectionBook)
        {
            if (id is null)
            {
                return NotFound();
            }

            bookCollectionBook.BookID = (int)id;

            if (ModelState.IsValid)
            {
                await _bookCollectionFacade.AddBookToCollection(bookCollectionBook);
                return RedirectToAction(nameof(Details), new { id = bookCollectionBook.BookCollectionID });
            }
            return View(bookCollectionBook);
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

        // GET: BookCollection/UserEditCollection/5
        public async Task<IActionResult> UserEditCollection(int? id)
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

            if (id == null)
            {
                return NotFound();
            }

            var bookCollection = await _bookCollectionFacade.GetUserCollectionToEdit((int)id);

            if (bookCollection == null || (bookCollection.UserId != userId))
            {
                return NotFound();
            }

            return View(bookCollection);
        }

        // POST: BookCollection/UserEditCollection/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEditCollection(int? id, [Bind("Title,Description,Id,UserId,CreationDate")] BookCollectionCreateDTO bookCollection)
        {
            if (id != bookCollection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bookCollectionFacade.UserEditCollection(bookCollection);
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
            int userId;
            if (User.Identity.Name is not null)
            {
                userId = int.Parse(User.Identity.Name);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var bookCollection = await _bookCollectionFacade.Get((int)id);

            if (bookCollection == null || (bookCollection.UserId != userId && User.Claims.FirstOrDefault(x => x.Value == GlobalConstants.AdminRoleName) != null))
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

            return RedirectToAction(nameof(UserCollections));
        }

        private async Task<bool> BookCollectionExists(int id)
        {
            var bookCollection = await _bookCollectionFacade.Get(id);
            return bookCollection != null;
        }
    }
}
