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
using BL.DTOs.Entities.EReaderInstance;
using MVCProject.Config;
using BL.DTOs.ConnectionTables;
using MVCProject.StateManager;
using Autofac;

namespace MVCProject.Controllers
{
    public class EReaderInstanceController : Controller
    {
        private readonly EReaderInstanceFacade _eReaderInstanceFacade;
        private ILifetimeScope _lifeTime;

        public EReaderInstanceController(ILifetimeScope lifeTime)
        {
            _lifeTime = lifeTime;
            _eReaderInstanceFacade = _lifeTime.Resolve<EReaderInstanceFacade>();
        }

        // GET: EReaderInstance
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _eReaderInstanceFacade.GetEReaderInstancePrevsBy(null, null, null, null, null));
        }

        // POST: EReaderInstance
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string description,
                                               string company,
                                               string model,
                                               int? memoryFrom,
                                               int? memoryTo)
        {
            ViewData["description"] = description;
            ViewData["company"] = company;
            ViewData["model"] = model;
            ViewData["memoryFrom"] = memoryFrom;
            ViewData["memoryTo"] = memoryTo;
 
            return View(await _eReaderInstanceFacade.GetEReaderInstancePrevsBy(description, company, model, memoryFrom, memoryTo));
        }

        // GET: UserEReaders
        [HttpGet]
        public async Task<IActionResult> UserEReaders()
        {
            int userId;
            if (User.Identity.Name is not null)
            {
                userId = int.Parse(User.Identity.Name);
            }
            else
            {
                return RedirectToAction(nameof(Index), "Home");
            }

            return View(await _eReaderInstanceFacade.GetEReaderInstancesByOwner(userId));
        }

        // GET: EReaderInstance/UserAddEBookInEReader
        public IActionResult UserAddEBookInEReader()
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

            var eReaders = _eReaderInstanceFacade.GetEReaderInstancesByOwner(userId).Result;

            ViewData["eReaders"] = eReaders;
            return View();
        }

        // POST: EReaderInstance/UserAddEBookInEReader
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserAddEBookInEReader(int? id, [Bind("EReaderInstanceID")] AddEBookInEReaderInstanceDTO eReaderEbook)
        {
            if (id is null)
            {
                return NotFound();
            }

            eReaderEbook.EBookID = (int)id;
            if (await _eReaderInstanceFacade.CheckIfAlreadyHasBook(eReaderEbook.EReaderInstanceID, id.Value))
            {
                return RedirectToAction(nameof(Details), new { id = eReaderEbook.EReaderInstanceID });
            }

            if (ModelState.IsValid)
            {
                await _eReaderInstanceFacade.AddEBook(eReaderEbook);
                return RedirectToAction(nameof(Details), new { id = eReaderEbook.EReaderInstanceID });
            }
            return View(eReaderEbook);
        }

        // GET: EReaderInstance/UserCreateEReader
        public IActionResult UserCreateEReader()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            ViewData["eReaderTemplates"] = _eReaderInstanceFacade.GetEReaderTemplates().Result;
            return View();
        }

        // POST: EReaderInstance/UserCreateEReader
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCreateEReader([Bind("Description, EReaderTemplateID")] EReaderInstanceCreateDTO eReaderInstance)
        {
            int userId;
            if (User.Identity.Name is not null)
            {
                userId = int.Parse(User.Identity.Name);
            }
            else
            {
                return RedirectToAction(nameof(Index), "Home");
            }

            if (ModelState.IsValid)
            {
                await _eReaderInstanceFacade.AddNewEReaderToUser(eReaderInstance, userId);
                return RedirectToAction(nameof(UserEReaders));
            }
            return View(eReaderInstance);
        }

        // GET: EReaderInstance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eReaderInstance = await GetWithReferences((int)id);
            if (eReaderInstance == null)
            {
                return NotFound();
            }

            var loadedBooks = await _eReaderInstanceFacade.GetEBookEReaderInstancesByEReaderInstance((int)id);
            eReaderInstance.BooksIncluded = loadedBooks;
            ViewData["eBooks"] = loadedBooks.Select(entry => entry.EBook);

            return View(eReaderInstance);
        }

        [HttpPost]
        public IActionResult Details(int eReaderToModifyId, int eBookToDeleteId)
        {
            _eReaderInstanceFacade.DeleteEBook(eReaderToModifyId, eBookToDeleteId);

            return RedirectToAction("Details", new { id = eReaderToModifyId });
        }

        // GET: EReaderInstance/Create
        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            return View();
        }

        // POST: EReaderInstance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,EreaderOwnerId,EReaderTemplateID,Id")] EReaderInstanceDTO eReaderInstance)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _eReaderInstanceFacade.Create(eReaderInstance);
                return RedirectToAction(nameof(Index));
            }

            return View(eReaderInstance);
        }

        // GET: EReaderInstance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !User.Identity.IsAuthenticated || User.Identity.Name == null)
            {
                return NotFound();
            }

            var eReaderInstance = await GetWithReferences((int)id);
            if (eReaderInstance == null || (int.Parse(User.Identity.Name) != eReaderInstance.EreaderOwnerId && !User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            eReaderInstance.BooksIncluded = await _eReaderInstanceFacade.GetEBookEReaderInstancesByEReaderInstance((int)id);

            return View(eReaderInstance);
        }

        // POST: EReaderInstance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Description,EreaderOwnerId,EReaderTemplateID,Id")] EReaderInstanceDTO eReaderInstance)
        {
            if (id != eReaderInstance.Id || !User.Identity.IsAuthenticated || User.Identity.Name == null || 
                (int.Parse(User.Identity.Name) != eReaderInstance.EreaderOwnerId && !User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _eReaderInstanceFacade.Update(eReaderInstance);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await EReaderInstanceExists(eReaderInstance.Id))
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
            return View(eReaderInstance);
        }

        // GET: EReaderInstance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !User.Identity.IsAuthenticated || User.Identity.Name == null)
            {
                return NotFound();
            }

            var eReaderInstance = await GetWithReferences((int)id);
            if (eReaderInstance == null || (int.Parse(User.Identity.Name) != eReaderInstance.EreaderOwnerId && !User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            return View(eReaderInstance);
        }

        // POST: EReaderInstance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.Identity.IsAuthenticated || User.Identity.Name == null)
            {
                return NotFound();
            }
            
            var eReaderInstance = await _eReaderInstanceFacade.Get(id);
            if (eReaderInstance == null || (int.Parse(User.Identity.Name) != eReaderInstance.EreaderOwnerId && !User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            _eReaderInstanceFacade.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EReaderInstanceExists(int id)
        {
            var eReaderInstance = await _eReaderInstanceFacade.Get(id);
            return eReaderInstance != null;
        }

        private async Task<EReaderInstanceDTO> GetWithReferences(int id)
        {
            var refsToLoad = new string[]
            {
                nameof(EReaderInstanceDTO.EReaderTemplate),
                nameof(EReaderInstanceDTO.Owner)
            };

            var collsToLoad = new string[]
            {
                nameof(EReaderInstanceDTO.Reservations)
            };

            return await _eReaderInstanceFacade.Get(id,refsToLoad,collsToLoad);
        }

        protected override void Dispose(bool disposing)
        {
            _lifeTime.Dispose();
            base.Dispose(disposing);
        }
    }
}
