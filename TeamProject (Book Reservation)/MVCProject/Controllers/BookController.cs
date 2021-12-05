using BL.DTOs.Entities.Book;
using BL.DTOs.Enums;
using BL.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Config;
using System;
using System.Threading.Tasks;

namespace MVCProject.Controllers
{
    public class BookController : Controller
    {
        private readonly BookFacade _bookFacade;

        public BookController(BookFacade bookFacade)
        {
            _bookFacade = bookFacade;
        }

        // GET: Book
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _bookFacade.GetBookPreviews(null, null, null, null, null, null, null, null, null));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string title,
                                               string authorName,
                                               string authorSurname,
                                               GenreTypeDTO[] genres,
                                               LanguageDTO? language,
                                               int? pageFrom,
                                               int? pageTo,
                                               DateTime? releaseFrom,
                                               DateTime? releaseTo)
        {
            ViewData["bookTitle"] = title;
            ViewData["authorName"] = authorName;
            ViewData["authorSurname"] = authorSurname;
            ViewData["genres"] = genres;
            ViewData["language"] = language;
            ViewData["pageFrom"] = pageFrom;
            ViewData["pageTo"] = pageTo;
            ViewData["releaseFrom"] = releaseFrom;
            ViewData["releaseTo"] = releaseTo;

            return View(await _bookFacade.GetBookPreviews(title, authorName, authorSurname, genres, language, pageFrom, pageTo, releaseFrom, releaseTo));
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collectToLoad = new[] { nameof(BookDTO.BookInstances), nameof(BookDTO.Reviews), nameof(BookDTO.Genres) };

            var book = await _bookFacade.Get((int)id, collectToLoad: collectToLoad);
            if (book == null)
            {
                return NotFound();
            }

            book.Authors = await _bookFacade.GetAuthorBooksByBook((int)id);

            return View(book);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,ISBN,PageCount,DateOfRelease,Language,Id")] BookDTO book)
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bookFacade.Create(book);
                return RedirectToAction(nameof(Details), new { id = book.Id });
            }

            return View(book);
        }

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            var book = await _bookFacade.Get((int)id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,ISBN,PageCount,DateOfRelease,Language,Id")] BookDTO book)
        {
            if (id != book.Id || !User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bookFacade.Update(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await BookExists(book.Id))
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
            return View(book);
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            var collectToLoad = new[] { nameof(BookDTO.BookInstances), nameof(BookDTO.Reviews), nameof(BookDTO.Genres) };

            var book = await _bookFacade.Get((int)id, collectToLoad: collectToLoad);
            if (book == null)
            {
                return NotFound();
            }

            book.Authors = await _bookFacade.GetAuthorBooksByBook((int)id);

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            _bookFacade.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BookExists(int id)
        {
            var book = await _bookFacade.Get(id);
            return book != null;
        }
    }
}
