using BL.DTOs.ConnectionTables;
using BL.DTOs.Entities.BookCollection;
using BL.Facades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Config;
using System.Threading.Tasks;

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
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            var bookCollections = await _bookCollectionFacade.GetAllBookCollections();

            return View(bookCollections.Item1);
        }

        // GET: BookCollection/UserCollections
        public async Task<IActionResult> UserCollections()
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            int userId = int.Parse(User.Identity.Name);

            return View(await _bookCollectionFacade.GetBookCollectionPrevsByUser(userId));
        }


        // GET: BookCollection/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || (!User.IsInRole(GlobalConstants.UserRoleName) && !User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            var bookCollection = await _bookCollectionFacade.GetBookCollectionWithBooksAndOwner(id.Value);
            if (bookCollection == null || (bookCollection.UserId != int.Parse(User.Identity.Name) && !User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            ViewData["books"] = await _bookCollectionFacade.GetBookPreviewsWithAuthorsForCollection(bookCollection);

            return View(bookCollection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int bookCollectionId, int bookToDeleteId)
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName) && !User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            var bookCollection = await _bookCollectionFacade.Get(bookCollectionId);
            if (bookCollection == null || (bookCollection.UserId != int.Parse(User.Identity.Name) && !User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            _bookCollectionFacade.DeleteBookFromCollection(bookCollectionId, bookToDeleteId);

            return RedirectToAction(nameof(Details), new { id = bookCollectionId });
        }

        // GET: BookCollection/UserCreateCollection
        [HttpGet]
        public IActionResult UserCreateCollection(int? id)
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            return View();
        }

        // POST: BookCollection/UserCreateCollection
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCreateCollection([Bind("Title,Description")] BookCollectionCreateDTO bookCollection)
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(bookCollection);
            }

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

        // GET: BookCollection/UserAddBookInCollection
        public async Task<IActionResult> UserAddBookInCollection(int? id)
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName) || id == null)
            {
                return NotFound();
            }

            bool existsBook = await _bookCollectionFacade.DoesBookExist(id.Value);
            if (!existsBook)
            {
                return NotFound();
            }

            int userId = int.Parse(User.Identity.Name);

            var collectionPrevs = await _bookCollectionFacade.GetBookCollectionPrevsByUser(userId);

            ViewData["collections"] = collectionPrevs;
            return View();
        }

        // POST: BookCollection/UserAddBookInCollection
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserAddBookInCollection(int? id, [Bind("BookCollectionID")] AddBookInCollectionDTO bookCollectionBook)
        {
            if (id is null || !User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            bool existsBook = await _bookCollectionFacade.DoesBookExist(id.Value);
            if (!existsBook)
            {
                return NotFound();
            }

            bookCollectionBook.BookID = (int)id;

            var bookCollection = await _bookCollectionFacade.Get(bookCollectionBook.BookCollectionID);
            if (bookCollection == null || (bookCollection.UserId != int.Parse(User.Identity.Name) && !User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bookCollectionFacade.AddBookToCollection(bookCollectionBook);
                return RedirectToAction(nameof(Details), new { id = bookCollectionBook.BookCollectionID });
            }

            return View(bookCollectionBook);
        }

        // GET: BookCollection/UserEditCollection/5
        public async Task<IActionResult> UserEditCollection(int? id)
        {
            if (id == null || !User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            int userId = int.Parse(User.Identity.Name);

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
            if (id != bookCollection.Id || !User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            int userId = int.Parse(User.Identity.Name);

            if (bookCollection == null || (bookCollection.UserId != userId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(bookCollection);
            }

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

            return RedirectToAction(nameof(Details), new { id = bookCollection.Id });
        }

        // GET: BookCollection/Create
        public IActionResult Create()
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            return View();
        }

        // POST: BookCollection/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,CreationDate,UserId,Id")] BookCollectionDTO bookCollection)
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

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
            if (id == null || !User.IsInRole(GlobalConstants.AdminRoleName))
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
            if (id != bookCollection.Id || !User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(bookCollection);
            }

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

            return RedirectToAction(nameof(Details), new { id = bookCollection.Id });
        }

        // GET: BookCollection/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || (!User.IsInRole(GlobalConstants.UserRoleName) && !User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            var bookCollection = await _bookCollectionFacade.Get(id.Value);
            if (bookCollection == null || (bookCollection.UserId != int.Parse(User.Identity.Name) && !User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            return View(bookCollection);
        }

        // POST: BookCollection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName) && !User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            var bookCollection = await _bookCollectionFacade.Get(id);
            if (bookCollection == null || (bookCollection.UserId != int.Parse(User.Identity.Name) && !User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            _bookCollectionFacade.Delete(id);

            if (User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(UserCollections));
            }
        }

        private async Task<bool> BookCollectionExists(int id)
        {
            var bookCollection = await _bookCollectionFacade.Get(id);
            return bookCollection != null;
        }
    }
}
