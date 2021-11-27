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

namespace MVCProject.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ReservationFacade _facade;

        public ReservationController(ReservationFacade facade)
        {
            _facade = facade;
        }

        // GET: Reservation
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Reservation/Details/5
        public async Task<IActionResult> Details(int id)
        {
            string[] refsToLoad = new string[]
            {
                nameof(ReservationDTO.User)
            };

            var reservation = await _facade.Get((int)id);           

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

        // GET: Reservation/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }                
            }

            return RedirectToAction(nameof(this.DetailsCurrent));
        }

        public IActionResult DeleteInProgressReservation()
        {
            StateKeeper.Instance.SetReservationInSession(this, null);

            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController));
        }

        // POST: Reservation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DateFrom,DateTill,UserID,EReaderID,Id")] ReservationDTO reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _facade.Update(reservation);
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
                return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ReservationExists(int id)
        {
            var reservation = await _facade.Get(id);
            return reservation != null;
        }
    }
}
