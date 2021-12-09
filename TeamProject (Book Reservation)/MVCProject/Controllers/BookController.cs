using Autofac;
using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.Book;
using BL.DTOs.Enums;
using BL.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Config;
using MVCProject.StateManager;
using System;
using System.Threading.Tasks;

namespace MVCProject.Controllers
{
    public class BookController : Controller
    {
        private readonly BookFacade _bookFacade;

        public BookController(BookFacade facade)
        {            
            _bookFacade = facade;
        }

        // GET: Book
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["eBook"] = false;
            return View(await _bookFacade.GetBookPreviews(null, null, null, null, null, null, null, null, null));
        }

        // POST: Book
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
            ViewData["eBook"] = false;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(int? bookId, int? genreId, int? authorId)
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            if (bookId == null)
            {
                return View();
            }

            if (genreId != null)
            {
                _bookFacade.RemoveGenreFromBook(bookId.Value, genreId.Value);
            }
            else if (authorId != null)
            {
                _bookFacade.RemoveAuthorFromBook(bookId.Value, authorId.Value);
            }                        

            return RedirectToAction(nameof(Details), new { id = bookId });
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

        public IActionResult AddGenreToBook(int? id)
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            StateKeeper.Instance.AddTillNextRequest(this, TempDataKeys.BookDTOId, id.Value);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGenreToBook([Bind("GenreID,BookID")] BookGenreDTO newBookGenreEntry)
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            StateKeeper.Instance.SaveSpecificObjectForNextRequest(this, TempDataKeys.BookDTOId);

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _bookFacade.AddGenreToBook(newBookGenreEntry);

            return RedirectToAction(nameof(Details), new { id = newBookGenreEntry.BookID });
        }

        public async Task<IActionResult> AddAuthorToBook(int? id)
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            // Put all ids
            ViewData["authors"] = await _bookFacade.GetAllAuthors();

            StateKeeper.Instance.AddTillNextRequest(this, TempDataKeys.BookDTOId, id.Value);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAuthorToBook([Bind("AuthorID,BookID")] AuthorBookDTO newBookGenreEntry)
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            StateKeeper.Instance.SaveSpecificObjectForNextRequest(this, TempDataKeys.BookDTOId);

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _bookFacade.AddAuthorToBook(newBookGenreEntry);

            return RedirectToAction(nameof(Details), new { id = newBookGenreEntry.BookID });
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
