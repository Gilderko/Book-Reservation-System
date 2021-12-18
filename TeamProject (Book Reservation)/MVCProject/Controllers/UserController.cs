using BL.DTOs.Entities.User;
using BL.Facades;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Config;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCProject.Controllers
{
    public class UserController : Controller
    {
        private readonly UserFacade _userFacade;

        public UserController(UserFacade facade)
        {
            _userFacade = facade;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            var users = await _userFacade.GetAllUsers();

            return View(users.Item1);
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userFacade.Get(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Surname,Email,HashedPassword,IsAdmin,Id")] UserDTO user)
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _userFacade.Create(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/MyAccount
        public async Task<IActionResult> MyAccount()
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            int id = int.Parse(User.Identity.Name);            

            var user = await _userFacade.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/MyAccount/AccountEdit
        public async Task<IActionResult> AccountEdit()
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            int id = int.Parse(User.Identity.Name);

            var user = await _userFacade.GetEditDTO(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/MyAccount/AccountEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AccountEdit([Bind("Name,Surname,Email,Password,Id")] UserEditDTO user)
        {
            if (!User.IsInRole(GlobalConstants.UserRoleName))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }
            
            try
            {
                _userFacade.UpdateCredentials(user);
            }
            catch (ArgumentException)
            {
                ModelState.AddModelError("Email", "Account with that email already exists!");
                return View(user);
            }

            return RedirectToAction(nameof(MyAccount));
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            var user = await _userFacade.Get(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Surname,Email,HashedPassword,IsAdmin,Id")] UserDTO user)
        {
            if (id != user.Id || !User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }
            
            try
            {
                _userFacade.Update(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExists(user.Id))
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

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            var collectionsToLoad = new string[]
            {
                nameof(UserDTO.Reservations)
            };

            var user = await _userFacade.Get(id.Value, null, collectionsToLoad);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int[] reservations)
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            await _userFacade.Delete(id,reservations);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> UserExists(int id)
        {
            var user = await _userFacade.Get(id);
            return user != null;
        }

        // GET: User/Register
        [HttpGet, ActionName("Register")]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: User/Register
        [HttpPost, ActionName("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync([Bind("Name,Surname,Email,Password")] UserCreateDTO user)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }
            
            try
            {
                //Here should be a check for existing user 
                await _userFacade.RegisterUserAsync(user);

                return RedirectToAction("Login", "User");
            }
            catch (Exception)
            {
                ModelState.AddModelError("Email", "Account with that email already exists!");
                return View(user);
            }
        }

        // GET: User/Login
        [HttpGet, ActionName("Login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // GET: User/Logout
        [HttpGet, ActionName("Logout")]
        public IActionResult Logout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // POST: User/Login
        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync([Bind("Email,Password")] UserLoginDTO userLogin)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(userLogin);
            }
            
            try
            {
                var user = await _userFacade.LoginAsync(userLogin);

                await CreateClaimsAndSignInAsync(user);

                return RedirectToAction("Index", "Home");
            }
            catch (UnauthorizedAccessException)
            {
                ModelState.AddModelError("Password", "Invalid credentials combination!");
                return View(userLogin);
            }
        }

        private async Task CreateClaimsAndSignInAsync(UserShowDTO user)
        {
            var claims = new List<Claim>
            {
                //Set User Identity Name to actual user Id - easier access with user connected operations
                new Claim(ClaimTypes.Name, user.Id.ToString())
            };
            
            if (user.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, GlobalConstants.UserRoleName));
                claims.Add(new Claim(ClaimTypes.Role, GlobalConstants.AdminRoleName));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, GlobalConstants.UserRoleName));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
        }
    }
}
