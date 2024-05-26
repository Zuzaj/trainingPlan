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

            var difficulties = await _context.Difficulties.ToListAsync();
            var trainingTypes = await _context.TrainingTypes.ToListAsync();
            // ViewBag.Difficulties = difficulties;
            // ViewBag.TrainingTypes = trainingTypes;

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

 [HttpGet("{id}/edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.Trainings.FindAsync(id);
            if (training == null)
            {
                return NotFound();
            }

            ViewBag.Difficulties = await _context.Difficulties.ToListAsync();
            ViewBag.TrainingTypes = await _context.TrainingTypes.ToListAsync();
            return View(training);
        }

        [HttpPost("{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Training training)
        {
            if (id != training.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(training);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingExists(training.Id))
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
            ViewBag.Difficulties = await _context.Difficulties.ToListAsync();
            ViewBag.TrainingTypes = await _context.TrainingTypes.ToListAsync();
            return View(training);
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.Trainings
                .Include(t => t.Difficulty)
                .Include(t => t.TrainingType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }
 [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var training = await _context.Trainings
                                     .Include(t => t.TrainingType)
                                     .FirstOrDefaultAsync(m => m.Id == id);
    if (training == null)
    {
        return NotFound();
    }

    return View(training);
}

// POST: PlanView/Delete/5
 [HttpPost("delete/{id}")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
     var training = await _context.Trainings.FindAsync(id);
    if (training == null)
    {
        return NotFound();
    }

    _context.Trainings.Remove(training);
    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
}
        private bool TrainingExists(int id)
        {
            return _context.Trainings.Any(e => e.Id == id);
        }
    
        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}
