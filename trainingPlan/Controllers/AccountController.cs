// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Security.Cryptography;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using trainingPlan.Models;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using System.Security.Cryptography;
// using System.Text;
// using System.Threading.Tasks;
// using trainingPlan.Models;




// public class AccountController : Controller
// {
//     private readonly AppDbContext _context;

//     public AccountController(AppDbContext context)
//     {
//         _context = context;
//     }

//     [HttpGet]
//     public IActionResult Login() => View();

//     [HttpPost]
//     public IActionResult Login(string username, string password)
//     {

//         if (username == "admin" && password == "admin")
//         {
//             //HttpContext.Session.SetString("Username", username);
//             HttpContext.Session.SetString("IsAdmin", "true");
//             HttpContext.Session.SetString("LoggedIn", "true");
//             return RedirectToAction("Index", "Home");
//         }
//         var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
//         if (user != null && VerifyPassword(password, user.PasswordHash))
//         {
//             HttpContext.Session.SetString("Username", username);
//             HttpContext.Session.SetString("LoggedIn", "true");
//             return RedirectToAction("Index", "Home");
//         }
//         return View();
//     }

//     public IActionResult Register()
//     {
//         return View();
//     }
//     public IActionResult Logout()
//     {
//         HttpContext.Session.Clear();
//         return RedirectToAction("Login");
//     }
//     [HttpPost]
//     public async Task<IActionResult> Register(User user)
//     {
//         if (ModelState.IsValid)
//         {
//             user.PasswordHash = HashPassword(user.PasswordHash);
//             _context.Users.Add(user);
//             await _context.SaveChangesAsync();

//             return RedirectToAction("Index");
//         }
//         return View(user);
//     }
//     private string HashPassword(string password)
//     {
//         using (SHA256 sha256 = SHA256.Create())
//         {
//             byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
//             StringBuilder builder = new StringBuilder();
//             foreach (byte b in bytes)
//             {
//                 builder.Append(b.ToString("x2"));
//             }
//             return builder.ToString();
//         }
//     }
//     private bool VerifyPassword(string password, string storedHash)
//     {
//         string hashOfInput = HashPassword(password);
//         return string.Equals(hashOfInput, storedHash, StringComparison.OrdinalIgnoreCase);
//     }
//     public async Task<IActionResult> Index()
//     {
//         var users = await _context.Users.ToListAsync();
//         return View(users);
//     }


//     private bool UserIsAdmin()
//     {
//         var userId = HttpContext.Session.GetString("UserId");
//         if (userId != null)
//         {
//             var user = _context.Users.Find(int.Parse(userId));
//             return user != null && user.Username == "admin";
//         }
//         return false;
//     }


// }

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using trainingPlan.Models;

namespace trainingPlan.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user, string password)
        {

            if (ModelState.IsValid)
            {
                user.PasswordHash = HashPassword(password);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                //HttpContext.Session.SetString("Username", username);
                HttpContext.Session.SetString("IsAdmin", "true");
                HttpContext.Session.SetString("LoggedIn", "true");
                return RedirectToAction("Index", "Home");
            }
            // Upewnij się, że wszystkie asynchroniczne operacje są poprawnie obsługiwane
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                HttpContext.Session.SetString("Username", username);
                HttpContext.Session.SetString("LoggedIn", "true");
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            string hashOfInput = HashPassword(password);
            return string.Equals(hashOfInput, storedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
