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
using BL.DTOs.Entities.Book;
using MVCProject.StateManager;
using BL.DTOs.Filters;
using BL.DTOs.Enums;

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
        public async Task<IActionResult> Index(string title,
                                               string author,
                                               LanguageDTO? language,
                                               int pageFrom,
                                               int pageTo,
                                               DateTime releaseFrom,
                                               DateTime releaseTo)
        {
            FilterDto filter = new FilterDto();

            if (language is not null)
            {
                filter = new FilterDto()
                {
                    Predicate = new PredicateDto(nameof(Book.Language), (int)language, Infrastructure.Query.Operators.ValueComparingOperator.Equal)
                };
            }


            if (language is not null)
            {
                filter = new FilterDto()
                {
                    Predicate = new PredicateDto(nameof(Book.Language), (int)language, Infrastructure.Query.Operators.ValueComparingOperator.Equal)
                };
            }
            else
            {
                filter = new FilterDto();
            }


            return View(_bookFacade.GetBookPreviews(filter).Result);
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
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

        // GET: Book/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,ISBN,PageCount,DateOfRelease,Language,Id")] BookDTO book)
        {
            if (ModelState.IsValid)
            {
                await _bookFacade.Create(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
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
            if (id != book.Id)
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
            if (id == null)
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

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
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
