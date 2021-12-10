using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BL.Facades;
using BL.DTOs.Entities.Author;
using BL.DTOs.Entities.Book;
using MVCProject.Config;
using Autofac;

namespace MVCProject.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AuthorFacade _facade;

        public AuthorController(AuthorFacade facade)
        {
            _facade = facade;
        }

        // GET: Author
        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            return View(await _facade.GetAuthorPreviews());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string name, string surname)
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            ViewData["name"] = name;
            ViewData["surname"] = surname;

            return View(await _facade.GetAuthorPreviews(name, surname));
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
                return RedirectToAction(nameof(Details), new { id = author.Id });
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
                return RedirectToAction(nameof(Index));
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
