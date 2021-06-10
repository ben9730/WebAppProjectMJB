using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppProjectMJB.Data;
using WebAppProjectMJB.Models;

namespace WebAppProjectMJB.Controllers
{
    public class UsersController : Controller
    {
        private readonly WebAppProjectMJBContext _context;

        public UsersController(WebAppProjectMJBContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }


        public async Task<IActionResult> Search(string query)
        {

            var webAppProjectMJBContext = _context.User.Where(g => g.Username.Contains(query) || g.Email.Contains(query) || g.Address.Contains(query) || g.FullName.Contains(query));
            return PartialView(await webAppProjectMJBContext.ToListAsync());
        }


        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        // GET: Users/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //we delete the type
        public async Task<IActionResult> Register([Bind("Id,Username,Password,Email,FullName,Address")] User user)
        {
            if (ModelState.IsValid)
            {
                //בודק שלא קיים שם משתמש זהה בסיס הנתונים במידה ולא קיים מחזיר נלל
                var notValidUser = _context.User.FirstOrDefault(us => us.Username == user.Username || us.Email == user.Email);


                //if we dont have this username in the DB we crate him
                if (notValidUser == null)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();

                    
                    var isUserValid = _context.User.FirstOrDefault(us => us.Username == user.Username && us.Password == user.Password);
                    //we use this to make auto login after register
                    Signin(isUserValid);

                    return RedirectToAction(nameof(Index), "Home");
                }
                else
                {
                    //viewdata is like var you can difine and use in the view
                    if(notValidUser.Username == user.Username && notValidUser.Email == user.Email)
                    {
                        ViewData["Error"] = "Cannot create this user, this username and Email is already exists in the system";
                    }
                    else if(notValidUser.Email == user.Email)
                    {
                        ViewData["Error"] = "Cannot create this user, this Email is already exists in the system";
                    }
                    else if(notValidUser.Username == user.Username)
                    {
                        ViewData["Error"] = "Cannot create this user, this username is already exists in the system";
                    }
                    //ViewData["Error"] = "cannot crate this user, problem with username or/and Email";
                }
            }
            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        // POST: Users/ Login
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login([Bind("Id,Username,Password,Email,FullName,Address")] User user)
        {
            if (ModelState.IsValid)
            {
                //בודק  שקיים שם משתמש זהה בסיס הנתונים במידה ולא קיים מחזיר נלל
                var isUserValid = _context.User.FirstOrDefault(us => us.Username == user.Username && us.Password == user.Password);
               

                //if we have this user in the DB
                if (isUserValid != null)
                {
                  
                    Signin(isUserValid);

                    return RedirectToAction(nameof(Index), "Home");
                }
                else
                {
                    //viewdata is like var you can difine and use in the view
                    ViewData["Error"] = "Username or Password are wrong";
                }
            }
            return View(user);
        }


        private async void Signin(User account)
        {
            var claims = new List<Claim>
            { 
                //this is the key -> val 
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Role, account.Type.ToString()),
            };
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
                , authProperties);
        }

        public async Task<IActionResult> Logout()
        {
            //how to logout
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }


       // [Authorize(Roles = "Admin")]
        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,Email,FullName,Address,Type")] User user)
        {
            
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
