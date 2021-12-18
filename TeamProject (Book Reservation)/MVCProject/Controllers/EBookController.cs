using BL.DTOs.Entities.EBook;
using BL.DTOs.Enums;
using BL.Facades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Config;
using MVCProject.Models;
using MVCProject.StateManager;
using MVCProject.StateManager.FilterStates;
using System;
using System.Threading.Tasks;

namespace MVCProject.Controllers
{
    public class EBookController : BaseController
    {
        private readonly EBookFacade _eBookFacade;

        public EBookController(EBookFacade facade)
        {
            _eBookFacade = facade;
        }

        // GET: EBook
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var ebookFilterState = TempData.Get<EBookFilterState>(TempDataKeys.EBookFilter.ToString());
            if (ebookFilterState == null)
            {
                ebookFilterState = new EBookFilterState();
            }

            SetViewDataForIndex(ebookFilterState);

            var model = await _eBookFacade.GetBookPreviews(page, PageSize, ebookFilterState.Title,
                ebookFilterState.AuthorName, ebookFilterState.AuthorSurname, ebookFilterState.Genres,
                ebookFilterState.Language, ebookFilterState.PageFrom, ebookFilterState.PageTo,
                ebookFilterState.ReleaseFrom, ebookFilterState.ReleaseTo, ebookFilterState.Format);

            var pagedModel = new PagedListViewModel<EBookPrevDTO>(new PaginationViewModel(page, model.Item2, PageSize),
                                                                 model.Item1);

            TempData.Put<EBookFilterState>(TempDataKeys.EBookFilter.ToString(), ebookFilterState);

            return View(pagedModel);
        }

        // POST: EBook
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int page,
                                               string title,
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
            page = 1;
            var ebookFilterState = new EBookFilterState()
            {
                Title = title,
                AuthorName = authorName,
                AuthorSurname = authorSurname,
                Genres = genres,
                Language = language,
                PageFrom = pageFrom,
                PageTo = pageTo,
                ReleaseFrom = releaseFrom,
                ReleaseTo = releaseTo,
                Format = format
            };

            SetViewDataForIndex(ebookFilterState);

            var model = await _eBookFacade.GetBookPreviews(page, PageSize, ebookFilterState.Title,
                ebookFilterState.AuthorName, ebookFilterState.AuthorSurname, ebookFilterState.Genres,
                ebookFilterState.Language, ebookFilterState.PageFrom, ebookFilterState.PageTo,
                ebookFilterState.ReleaseFrom, ebookFilterState.ReleaseTo, ebookFilterState.Format);

            var pagedModel = new PagedListViewModel<EBookPrevDTO>(new PaginationViewModel(page, model.Item2, PageSize),
                                                                 model.Item1);

            TempData.Put<EBookFilterState>(TempDataKeys.EBookFilter.ToString(), ebookFilterState);

            return View(pagedModel);
        }

        private void SetViewDataForIndex(EBookFilterState bookFilterState)
        {
            ViewData["eBook"] = true;
            ViewData["bookTitle"] = bookFilterState.Title;
            ViewData["authorName"] = bookFilterState.AuthorName;
            ViewData["authorSurname"] = bookFilterState.AuthorSurname;
            ViewData["genres"] = bookFilterState.Genres;
            ViewData["language"] = bookFilterState.Language;
            ViewData["pageFrom"] = bookFilterState.PageFrom;
            ViewData["pageTo"] = bookFilterState.PageTo;
            ViewData["releaseFrom"] = bookFilterState.ReleaseFrom;
            ViewData["releaseTo"] = bookFilterState.ReleaseTo;
            ViewData["format"] = bookFilterState.Format;
        }

        // GET: EBook/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eBook = await _eBookFacade.Get(id.Value);
            if (eBook == null)
            {
                return NotFound();
            }

            return View(eBook);
        }

        // GET: EBook/Create
        public IActionResult Create()
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            return View();
        }

        // POST: EBook/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemorySize,Format,Title,Description,ISBN,PageCount,DateOfRelease,Language,Id")] EBookDTO eBook)
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

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
            if (id == null || !User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            var eBook = await _eBookFacade.Get(id.Value);
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
            if (id != eBook.Id || !User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(eBook);
            }
            
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

            return RedirectToAction(nameof(Details), new { id = eBook.Id });
        }

        // GET: EBook/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            var eBook = await _eBookFacade.Get(id.Value);
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
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

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
