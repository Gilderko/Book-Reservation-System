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
using BL.DTOs.Entities.Reservation;
using MVCProject.StateManager;
using BL.DTOs.ConnectionTables;
using MVCProject.Config;
using Autofac;

namespace MVCProject.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ReservationFacade _facade;
        private ILifetimeScope _lifeTime;

        public ReservationController(ILifetimeScope lifeTime)
        {
            _lifeTime = lifeTime;
            _facade = _lifeTime.Resolve<ReservationFacade>();
        }

        // GET: Reservation
        public async Task<IActionResult> Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            if (!HttpContext.User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            return View(await _facade.Index());
        }

        public async Task<IActionResult> UserReservations()
        {
            int id;

            if (User.Identity.Name is not null)
            {
                id = int.Parse(User.Identity.Name);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View(await _facade.GetReservationsByUserId(id));
        }

        // GET: Reservation/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var reservation = await _facade.GetDetailWithLoadedBooks(id);

            if (reservation == null)
            {
                return NotFound();
            }

            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            if (!(int.Parse(HttpContext.User.Identity.Name) == reservation.UserID || HttpContext.User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            return View(reservation);
        }

        public IActionResult DetailsCurrent()
        {
            var reservation = StateKeeper.Instance.GetReservationInSession(this);

            if (reservation == null)
            {
                return NotFound();
            }

            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            if (int.Parse(HttpContext.User.Identity.Name) != reservation.UserID)
            {
                return NotFound();
            }

            return View(reservation);
        }

        public async Task<IActionResult> AddedBookInstance(int id)
        {
            var reservation = StateKeeper.Instance.GetReservationInSession(this);

            if (reservation == null)
            {
                return NotFound();
            }

            await _facade.AddBookInstance(id, reservation);
            StateKeeper.Instance.SetReservationInSession(this, reservation);

            return RedirectToAction(nameof(this.DetailsCurrent));
        }

        public async Task<IActionResult> AddedEReaderInstance(int id)
        {
            var reservation = StateKeeper.Instance.GetReservationInSession(this);

            if (reservation == null)
            {
                return NotFound();
            }

            await _facade.AddEReaderInstance(id, reservation);
            StateKeeper.Instance.SetReservationInSession(this, reservation);

            return RedirectToAction(nameof(this.DetailsCurrent));
        }

        // GET: Reservation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("DateFrom,DateTill,Id")] ReservationDTO reservation)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            reservation.BookInstances = new List<ReservationBookInstanceDTO>();
            reservation.UserID = int.Parse(HttpContext.User.Identity.Name);

            if (ModelState.IsValid)
            {
                StateKeeper.Instance.SetReservationInSession(this, reservation);
                return RedirectToAction(nameof(this.DetailsCurrent));
            }

            return RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> SubmitReservation()
        {
            var reservation = StateKeeper.Instance.GetReservationInSession(this);

            if (reservation == null)
            {
                return NotFound();
            }

            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            if (int.Parse(HttpContext.User.Identity.Name) != reservation.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var succeded = await _facade.SubmitReservation(reservation);
                if (succeded)
                {
                    StateKeeper.Instance.SetReservationInSession(this, null);
                    return RedirectToAction(nameof(this.UserReservations));
                }
            }

            return RedirectToAction(nameof(this.DetailsCurrent));
        }

        public IActionResult DeleteInProgressReservation()
        {
            StateKeeper.Instance.SetReservationInSession(this, null);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        // GET: Reservation/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var reservation = await _facade.GetDetailWithLoadedBooks(id);

            if (reservation == null)
            {
                return NotFound();
            }

            if (!(int.Parse(HttpContext.User.Identity.Name) == reservation.UserID || HttpContext.User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            return View(reservation);
        }        

        // POST: Reservation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("DateFrom,DateTill,UserID,EReaderID,Id")] ReservationDTO reservation, int[] booksToDelete, int? eReaderInstanceDelete)
        {
            if (!(int.Parse(HttpContext.User.Identity.Name) == reservation.UserID || HttpContext.User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            var collectToLoad = new string[]
            {
                nameof(ReservationDTO.BookInstances)
            };          

            if (ModelState.IsValid)
            {
                try
                {
                    _facade.RemoveBookInstances(reservation, booksToDelete);
                    if (eReaderInstanceDelete.HasValue)
                    {
                        _facade.RemoveEReaderInstance(reservation);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = reservation.Id } );
            }
            return View(reservation);
        }       

        // GET: Reservation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _facade.Get((int)id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _facade.Delete(id);
            return RedirectToAction(nameof(UserReservations));
        }

        private async Task<bool> ReservationExists(int id)
        {
            var reservation = await _facade.Get(id);
            return reservation != null;
        }

        protected override void Dispose(bool disposing)
        {
            _lifeTime.Dispose();
            base.Dispose(disposing);
        }
    }
}
