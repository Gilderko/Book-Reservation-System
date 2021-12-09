using Autofac;
using BL.DTOs.Entities.EBook;
using BL.DTOs.Enums;
using BL.Facades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MVCProject.Controllers
{
    public class EBookController : Controller
    {
        private readonly EBookFacade _eBookFacade;

        public EBookController(EBookFacade facade)
        {
            _eBookFacade = facade;
        }

        // GET: EBook
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["eBook"] = true;
            return View(await _eBookFacade.GetBookPreviews(null, null, null, null, null, null, null, null, null, null));
        }

        // POST: EBook
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
                                               DateTime? releaseTo,
                                               EBookFormatDTO? format)
        {
            ViewData["eBook"] = true;
            ViewData["bookTitle"] = title;
            ViewData["authorName"] = authorName;
            ViewData["authorSurname"] = authorSurname;
            ViewData["genres"] = genres;
            ViewData["language"] = language;
            ViewData["pageFrom"] = pageFrom;
            ViewData["pageTo"] = pageTo;
            ViewData["releaseFrom"] = releaseFrom;
            ViewData["releaseTo"] = releaseTo;
            ViewData["format"] = format;

            return View(await _eBookFacade.GetBookPreviews(title, authorName, authorSurname, genres, language, pageFrom, pageTo, releaseFrom, releaseTo, format));
        }

        // GET: EBook/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eBook = await _eBookFacade.Get((int)id);
            if (eBook == null)
            {
                return NotFound();
            }

            return View(eBook);
        }

        // GET: EBook/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EBook/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemorySize,Format,Title,Description,ISBN,PageCount,DateOfRelease,Language,Id")] EBookDTO eBook)
        {
            if (ModelState.IsValid)
            {
                await _eBookFacade.Create(eBook);
                return RedirectToAction(nameof(Index));
            }
            return View(eBook);
        }

        // GET: EBook/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eBook = await _eBookFacade.Get((int)id);
            if (eBook == null)
            {
                return NotFound();
            }
            return View(eBook);
        }

        // POST: EBook/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemorySize,Format,Title,Description,ISBN,PageCount,DateOfRelease,Language,Id")] EBookDTO eBook)
        {
            if (id != eBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _eBookFacade.Update(eBook);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await EBookExists(eBook.Id))
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
            return View(eBook);
        }

        // GET: EBook/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eBook = await _eBookFacade.Get((int)id);
            if (eBook == null)
            {
                return NotFound();
            }

            return View(eBook);
        }

        // POST: EBook/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _eBookFacade.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EBookExists(int id)
        {
            var eBook = await _eBookFacade.Get(id);
            return eBook != null;
        }
    }
}
