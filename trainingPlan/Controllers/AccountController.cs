using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using trainingPlan.Models;


public class AccountController : Controller
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == username);
        if (user != null && VerifyPasswordHash(password, user.PasswordHash))
        {
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    private bool VerifyPasswordHash(string password, string storedHash)
    {
        using var hmac = new HMACSHA512();
        var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return storedHash == Convert.ToBase64String(hash);
    }
}