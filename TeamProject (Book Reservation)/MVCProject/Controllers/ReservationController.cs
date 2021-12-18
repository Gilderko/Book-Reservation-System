using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BL.Facades;
using BL.DTOs.Entities.Reservation;
using MVCProject.StateManager;
using BL.DTOs.ConnectionTables;
using MVCProject.Config;

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
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            var reservations = await _facade.GetAllReservations();

            return View(reservations.Item1);
        }

        public async Task<IActionResult> UserReservations()
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            int id = int.Parse(User.Identity.Name);            

            return View(await _facade.GetReservationsByUserId(id));
        }

        // GET: Reservation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || (!User.IsInRole(GlobalConstants.UserRoleName) && !User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            var reservation = await _facade.GetDetailWithLoadedBooks(id.Value);

            if (reservation == null || 
                !(int.Parse(User.Identity.Name) == reservation.UserID || User.IsInRole(GlobalConstants.AdminRoleName)))
            {
                return NotFound();
            }

            return View(reservation);
        }

        public IActionResult DetailsCurrent()
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            var reservation = StateKeeper.Instance.GetReservationInSession(this);

            if (reservation == null || int.Parse(User.Identity.Name) != reservation.UserID)
            {
                return NotFound();
            }            

            return View(reservation);
        }

        public async Task<IActionResult> AddedBookInstance(int id)
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            var reservation = StateKeeper.Instance.GetReservationInSession(this);

            if (reservation == null)
            {
                return NotFound();
            }

            await _facade.AddBookInstance(id, reservation);
            StateKeeper.Instance.SetReservationInSession(this, reservation);

            return RedirectToAction(nameof(DetailsCurrent));
        }

        public async Task<IActionResult> AddedEReaderInstance(int id)
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            var reservation = StateKeeper.Instance.GetReservationInSession(this);

            if (reservation == null)
            {
                return NotFound();
            }

            await _facade.AddEReaderInstance(id, reservation);
            StateKeeper.Instance.SetReservationInSession(this, reservation);

            return RedirectToAction(nameof(DetailsCurrent));
        }

        // GET: Reservation/Create
        public IActionResult Create()
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            return View();
        }

        // POST: Reservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("DateFrom,DateTill,Id")] ReservationDTO reservation)
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            reservation.BookInstances = new List<ReservationBookInstanceDTO>();
            reservation.UserID = int.Parse(HttpContext.User.Identity.Name);

            if (reservation.DateFrom <= reservation.DateTill && ModelState.IsValid)
            {
                StateKeeper.Instance.SetReservationInSession(this, reservation);
                return RedirectToAction(nameof(DetailsCurrent));
            }

            return View(reservation);
        }

        public async Task<IActionResult> SubmitReservation()
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            var reservation = StateKeeper.Instance.GetReservationInSession(this);

            if (reservation == null || int.Parse(User.Identity.Name) != reservation.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var succeded = await _facade.SubmitReservation(reservation);
                if (succeded)
                {
                    StateKeeper.Instance.SetReservationInSession(this, null);
                    return RedirectToAction(nameof(UserReservations));
                }
            }

            return RedirectToAction(nameof(DetailsCurrent));
        }

        public IActionResult DeleteInProgressReservation()
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            StateKeeper.Instance.SetReservationInSession(this, null);

            return RedirectToAction(nameof(UserReservations));
        }

        // GET: Reservation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            var reservation = await _facade.GetDetailWithLoadedBooks(id.Value);

            if (reservation == null)
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
        public async Task<IActionResult> Edit(int id, [Bind("DateFrom,DateTill,UserID,EReaderID,Id")] ReservationDTO reservation, int[] booksToDelete, int? eReaderInstanceDelete)
        {
            if (id != reservation.Id || !User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            var collectToLoad = new string[]
            {
                nameof(ReservationDTO.BookInstances)
            };          

            if (!ModelState.IsValid)
            {
                return View(reservation);
            }
            
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

            return RedirectToAction(nameof(Details), new { id = reservation.Id });
        }       

        // GET: Reservation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || (!User.IsInRole(GlobalConstants.AdminRoleName) && !User.IsInRole(GlobalConstants.UserRoleName)))
            {
                return NotFound();
            }

            var reservation = await _facade.GetDetailWithLoadedBooks(id.Value);
            if (reservation == null ||
                !(int.Parse(User.Identity.Name) == reservation.UserID || User.IsInRole(GlobalConstants.AdminRoleName)))
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
            if (!User.IsInRole(GlobalConstants.AdminRoleName) && !User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            _facade.Delete(id);     
            
            if (User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(UserReservations));
            }
        }

        private async Task<bool> ReservationExists(int id)
        {
            var reservation = await _facade.Get(id);
            return reservation != null;
        }
    }
}
