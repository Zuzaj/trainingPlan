
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trainingPlan.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace trainingPlan.Controllers
{
    public class PlanViewController : Controller
    {
        private readonly AppDbContext _context;

        public PlanViewController(AppDbContext context)
        {
            _context = context;

        }

        // GET: PlanView
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var plans = await _context.PlanViews
                                       .Where(p => p.UserId == userId)
                                       .Include(p => p.Trainings)
                                       .ToListAsync();
            return View(plans);
        }

        // GET: PlanView/Create
        public IActionResult Create()
        {
            ViewBag.Trainings = new MultiSelectList(_context.Trainings, "Id", "Name");
            return View();
        }

        //POST: PlanView/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeekStart,TotalDuration,Comments,TrainingIds")] PlanView planView)
        {
            planView.UserId = GetUserId();

            if (ModelState.IsValid)
            {
                if (planView.TrainingIds != null && planView.TrainingIds.Any())
                {
                    planView.Trainings = await _context.Trainings
                        .Where(t => planView.TrainingIds.Contains(t.Id))
                        .ToListAsync();
                }
                CalculateTotalDuration(planView);
                _context.Add(planView);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Trainings = new MultiSelectList(_context.Trainings, "Id", "Name", planView.TrainingIds);
            return View(planView);
        }


        private bool PlanViewExists(int id)
        {
            return _context.PlanViews.Any(e => e.Id == id);
        }


        private int GetUserId()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int userId))
            {
                return userId;
            }
            throw new Exception("User is not logged in");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planView = await _context.PlanViews
                                         .Include(p => p.Trainings)
                                         .FirstOrDefaultAsync(m => m.Id == id);
            if (planView == null)
            {
                return NotFound();
            }

            return View(planView);
        }

        private bool PlanExists(int id)
        {
            return _context.PlanViews.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planView = await _context.PlanViews
                                         .Include(p => p.Trainings)
                                         .FirstOrDefaultAsync(m => m.Id == id);
            if (planView == null)
            {
                return NotFound();
            }

            ViewBag.Trainings = new MultiSelectList(_context.Trainings, "Id", "Name", planView.Trainings.Select(t => t.Id));
            return View(planView);
        }

        // POST: PlanView/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WeekStart,TotalDuration,Comments,TrainingIds")] PlanView planView)
        {
            if (id != planView.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (planView.TrainingIds != null && planView.TrainingIds.Any())
                    {
                        planView.Trainings = await _context.Trainings
                            .Where(t => planView.TrainingIds.Contains(t.Id))
                            .ToListAsync();
                    }

                    var originalPlan = await _context.PlanViews
                        .Include(p => p.Trainings)
                        .FirstOrDefaultAsync(p => p.Id == id);

                    if (originalPlan == null)
                    {
                        return NotFound();
                    }

                    originalPlan.WeekStart = planView.WeekStart;
                    originalPlan.Comments = planView.Comments;
                    originalPlan.Trainings = planView.Trainings;
                    originalPlan.TrainingIds = planView.TrainingIds;
                    CalculateTotalDuration(originalPlan);
                    _context.Update(originalPlan);

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanViewExists(planView.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewBag.Trainings = new MultiSelectList(_context.Trainings, "Id", "Name", planView.Trainings.Select(t => t.Id));
            return View(planView);
        }
        // GET: PlanView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planView = await _context.PlanViews
                                         .FirstOrDefaultAsync(m => m.Id == id);
            if (planView == null)
            {
                return NotFound();
            }

            return View(planView);
        }

        // POST: PlanView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planView = await _context.PlanViews.FindAsync(id);
            if (planView == null)
            {
                return NotFound();
            }

            _context.PlanViews.Remove(planView);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private void CalculateTotalDuration(PlanView planView)
        {
            int totalDuration = 0;
            foreach (var trainingId in planView.TrainingIds)
            {
                var training = _context.Trainings.Find(trainingId);
                if (training != null)
                {
                    totalDuration += training.Duration;
                }
            }
            planView.TotalDuration = totalDuration;
        }
    }
}
