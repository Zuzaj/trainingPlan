using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trainingPlan.Models;
using trainingPlan.Models;

namespace trainingPlan.Controllers
{
    [Route("[controller]")]
    public class PlanViewController : Controller
    {
        private readonly AppDbContext _context;

        public PlanViewController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var plans = await _context.Plans.Include(p => p.Trainings).ToListAsync();
            return View(plans);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Trainings = await _context.Trainings.ToListAsync();
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(PlanView model)
        {
            if (ModelState.IsValid)
            {
                var plan = new Plan
                {
                    UserId = model.UserId,
                    WeekStart = model.WeekStart,
                    TotalDuration = _context.Trainings
                        .Where(t => model.TrainingIds.Contains(t.Id))
                        .Sum(t => t.Duration),
                    Trainings = await _context.Trainings
                        .Where(t => model.TrainingIds.Contains(t.Id))
                        .ToListAsync(),
                    Comments = model.Comments
                };

                _context.Add(plan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Trainings = await _context.Trainings.ToListAsync();
            return View(model);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var plan = await _context.Plans
                .Include(p => p.Trainings)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var plan = await _context.Plans
                .Include(p => p.Trainings)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (plan == null)
            {
                return NotFound();
            }

            var model = new PlanView
            {
                Id = plan.Id,
                UserId = plan.UserId,
                WeekStart = plan.WeekStart,
                TrainingIds = plan.Trainings.Select(t => t.Id).ToList(),
                Comments = plan.Comments
            };

            ViewBag.Trainings = await _context.Trainings.ToListAsync();
            return View(model);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, PlanView model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var plan = await _context.Plans.Include(p => p.Trainings).FirstOrDefaultAsync(p => p.Id == id);

                    if (plan == null)
                    {
                        return NotFound();
                    }

                    plan.UserId = model.UserId;
                    plan.WeekStart = model.WeekStart;
                    plan.TotalDuration = _context.Trainings
                        .Where(t => model.TrainingIds.Contains(t.Id))
                        .Sum(t => t.Duration);
                    plan.Trainings = await _context.Trainings
                        .Where(t => model.TrainingIds.Contains(t.Id))
                        .ToListAsync();
                    plan.Comments = model.Comments;

                    _context.Update(plan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanExists(model.Id))
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
            ViewBag.Trainings = await _context.Trainings.ToListAsync();
            return View(model);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var plan = await _context.Plans.FindAsync(id);
            if (plan == null)
            {
                return NotFound();
            }
            return View(plan);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plan = await _context.Plans.FindAsync(id);
            _context.Plans.Remove(plan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanExists(int id)
        {
            return _context.Plans.Any(e => e.Id == id);
        }
    }
}
