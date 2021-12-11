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

        public EReaderInstanceController(EReaderInstanceFacade facade)
        {
            _eReaderInstanceFacade = facade;
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
            if (!User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            int userId = int.Parse(User.Identity.Name);

            return View(await _eReaderInstanceFacade.GetEReaderInstancesByOwner(userId));
        }

        // GET: EReaderInstance/UserAddEBookInEReader
        public IActionResult UserAddEBookInEReader()
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            int userId = int.Parse(User.Identity.Name);

            var eReaders = _eReaderInstanceFacade.GetEReaderInstancesByOwner(userId).Result;

            ViewData["eReaders"] = eReaders;
            return View();
        }

        // POST: EReaderInstance/UserAddEBookInEReader
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserAddEBookInEReader(int? id, [Bind("EReaderInstanceID")] AddEBookInEReaderInstanceDTO eReaderEbook)
        {
            if (id is null || !User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            var eReaderExists = await EReaderInstanceExists(eReaderEbook.EReaderInstanceID);
            if (!eReaderExists)
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
            if (!User.IsInRole(GlobalConstants.UserRoleName))
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
            if (!User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            int userId = int.Parse(User.Identity.Name);            

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
        public async Task<IActionResult> Details(int eReaderToModifyId, int eBookToDeleteId)
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName) && !User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            };

            var eReaderInstance = await _eReaderInstanceFacade.Get(eReaderToModifyId);
            if (eReaderInstance == null || (eReaderInstance.EreaderOwnerId != int.Parse(User.Identity.Name) 
                && !User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            _eReaderInstanceFacade.DeleteEBook(eReaderToModifyId, eBookToDeleteId);

            return RedirectToAction("Details", new { id = eReaderToModifyId });
        }

        // GET: EReaderInstance/Create
        public IActionResult Create()
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            };

            return View();
        }

        // POST: EReaderInstance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,EreaderOwnerId,EReaderTemplateID,Id")] EReaderInstanceDTO eReaderInstance)
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            };

            if (ModelState.IsValid)
            {
                await _eReaderInstanceFacade.Create(eReaderInstance);
                return RedirectToAction(nameof(Index));
            }

            return View(eReaderInstance);
        }

        // GET: EReaderInstance/UserEditBookInstance/5
        public async Task<IActionResult> UserEditEReaderInstance(int? id)
        {
            if (id == null || !User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            var eReaderInstance = await GetWithReferences((int)id);
            if (eReaderInstance == null || (int.Parse(User.Identity.Name) != eReaderInstance.EreaderOwnerId))
            {
                return NotFound();
            }

            eReaderInstance.BooksIncluded = await _eReaderInstanceFacade.GetEBookEReaderInstancesByEReaderInstance((int)id);

            return View(eReaderInstance);
        }

        // POST: EReaderInstance/UserEditBookInstance/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEditEReaderInstance(int id, [Bind("Description,EreaderOwnerId,EReaderTemplateID,Id")] EReaderInstanceDTO eReaderInstance)
        {
            if (id != eReaderInstance.Id || !User.IsInRole(GlobalConstants.UserRoleName) || 
                (int.Parse(User.Identity.Name) != eReaderInstance.EreaderOwnerId))
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
                return RedirectToAction(nameof(Details), new { id = eReaderInstance.Id});
            }
            return View(eReaderInstance);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            var eReaderInstance = await GetWithReferences((int)id);
            if (eReaderInstance == null)
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
            if (id != eReaderInstance.Id || !User.IsInRole(GlobalConstants.AdminRoleName))
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
                return RedirectToAction(nameof(Details), new { id = eReaderInstance.Id });
            }
            return View(eReaderInstance);
        }

        // GET: EReaderInstance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || (!User.IsInRole(GlobalConstants.AdminRoleName) && !User.IsInRole(GlobalConstants.UserRoleName)))
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
            if (!User.IsInRole(GlobalConstants.AdminRoleName) && !User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            var eReaderInstance = await _eReaderInstanceFacade.Get(id);
            if (eReaderInstance == null || (int.Parse(User.Identity.Name) != eReaderInstance.EreaderOwnerId && !User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            _eReaderInstanceFacade.Delete(id);

            if (User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(UserEReaders));
            }
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
    }
}
