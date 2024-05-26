

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using trainingPlan.Models;
using Microsoft.Extensions.Logging;

namespace trainingPlan.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(AppDbContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Account/Register


        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register

        // // GET: Account/Register


        // POST: Account/Register

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            // Hash the password and set the PasswordHash property
            user.PasswordHash = HashPassword(user.Password);
            user.Password = string.Empty; // Clear the plain password

            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is not valid.");
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        _logger.LogError($"Key: {key}, Error: {error.ErrorMessage}");
                    }
                }
                return View(user);
            }

            // Check if the username already exists
            var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Username == user.Username);
            if (existingUser != null)
            {
                ModelState.AddModelError("Username", "Username is already taken.");
                _logger.LogError("Username is already taken.");
                return View(user);
            }

            try
            {
                // Add the user to the database
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("User added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding user: {ex.Message}");
                throw;
            }

            return RedirectToAction(nameof(Index));
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
                HttpContext.Session.SetString("IsAdmin", "true");
                HttpContext.Session.SetString("LoggedIn", "true");
                return RedirectToAction("Index", "Home");
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
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
