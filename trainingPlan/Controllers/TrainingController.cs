using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trainingPlan.Models;

namespace trainingPlan.Controllers
{
    [Route("[controller]")]
    public class TrainingController : Controller
    {
        private readonly AppDbContext _context;

        public TrainingController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            // return _context.Trainings != null ?
            //     View(await _context.Trainings.Include(t => t.Difficulty).Include(t => t.TrainingType).ToListAsync()) :
            //     Problem("Entity set is null.");
            var difficulties = await _context.Difficulties.ToListAsync();
            var trainingTypes = await _context.TrainingTypes.ToListAsync();
            // Tymczasowy kod do sprawdzenia wartości
            ViewBag.Difficulties = difficulties;
            ViewBag.TrainingTypes = trainingTypes;

            return _context.Trainings != null ?
                View(await _context.Trainings.Include(t => t.Difficulty).Include(t => t.TrainingType).ToListAsync()) :
                Problem("Entity set is null.");
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Difficulties = await _context.Difficulties.ToListAsync();
            ViewBag.TrainingTypes = await _context.TrainingTypes.ToListAsync();
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Training training)
        {
            if (ModelState.IsValid)
            {
                _context.Add(training);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Difficulties = await _context.Difficulties.ToListAsync();
            ViewBag.TrainingTypes = await _context.TrainingTypes.ToListAsync();
            return View(training);
        }

        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}
