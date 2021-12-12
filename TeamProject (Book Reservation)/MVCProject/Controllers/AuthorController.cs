using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BL.Facades;
using BL.DTOs.Entities.Author;
using BL.DTOs.Entities.Book;
using MVCProject.Config;
using Autofac;
using MVCProject.Models;
using MVCProject.StateManager.FilterStates;
using MVCProject.StateManager;

namespace MVCProject.Controllers
{
    public class AuthorController : BaseController
    {
        private readonly AuthorFacade _facade;        

        public AuthorController(AuthorFacade facade)
        {
            _facade = facade;
        }

        // GET: Author
        public async Task<IActionResult> Index(int page = 1)
        {
            var authorFilterState = TempData.Get<AuthorFilterState>(TempDataKeys.AuthorFilter.ToString());
            if (authorFilterState == null)
            {
                authorFilterState = new AuthorFilterState();
            }

            SetViewDataForIndex(authorFilterState);

            var model = await _facade.GetAuthorPreviews(page, PageSize, authorFilterState.Name, authorFilterState.Surname);


            var pagedModel = new PagedListViewModel<AuthorPrevDTO>(new PaginationViewModel(page, model.Item2, PageSize),
                                                                 model.Item1);

            TempData.Put<AuthorFilterState>(TempDataKeys.AuthorFilter.ToString(), authorFilterState);
            return View(pagedModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int page, string name, string surname)
        {
            page = 1;
            var authorFilterState = new AuthorFilterState()
            {
                Name = name,
                Surname = surname
            };

            SetViewDataForIndex(authorFilterState);

            var model = await _facade.GetAuthorPreviews(page, PageSize, authorFilterState.Name, authorFilterState.Surname);


            var pagedModel = new PagedListViewModel<AuthorPrevDTO>(new PaginationViewModel(page, model.Item2, PageSize),
                                                                 model.Item1);

            TempData.Put<AuthorFilterState>(TempDataKeys.AuthorFilter.ToString(), authorFilterState);

            return View(pagedModel);
        }

        private void SetViewDataForIndex(AuthorFilterState authorFilterState)
        {
            ViewData["name"] = authorFilterState.Name;
            ViewData["surname"] = authorFilterState.Surname;
        }

        // GET: Author/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _facade.Get((int)id);
            if (author == null)
            {
                return NotFound();
            }

            author.AuthorsBooks = await _facade.GetAuthorBooksByAuthor((int)id);

            return View(author);
        }

        // GET: Author/Create
        public IActionResult Create()
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            return View();
        }

        // POST: Author/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorDTO author)
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _facade.Create(author);
                return RedirectToAction(nameof(Index));
            }

            return View(author);
        }

        // GET: Author/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            var author = await _facade.Get((int)id);
            if (author == null)
            {
                return NotFound();
            }

            author.AuthorsBooks = await _facade.GetAuthorBooksByAuthor((int)id);

            return View(author);
        }

        // POST: Author/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Surname,Description,Id")] AuthorDTO author)
        {
            if (id != author.Id || !User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _facade.Update(author);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AuthorExists(author.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = id });
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            var author = await _facade.Get((int)id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            _facade.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AuthorExists(int id)
        {
            var author = await _facade.Get(id);
            return author != null;
        }
    }
}
