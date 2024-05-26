// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using trainingPlan.Models;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using System.Security.Claims;

// namespace trainingPlan.Controllers
// {
//     public class PlanViewController : Controller
//     {
//         private readonly AppDbContext _context;

//         public PlanViewController(AppDbContext context)
//         {
//             _context = context;
//         }

//         // GET: PlanView
//         public async Task<IActionResult> Index()
//         {
//             var userId = GetUserId();
//             var plans = await _context.PlanViews
//                                        .Where(p => p.UserId == userId)
//                                       .Include(p => p.Trainings)
//                                       .ToListAsync();
//             return View(plans);
//         }

//         // GET: PlanView/Details/5
//         public async Task<IActionResult> Details(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var planView = await _context.PlanViews
//                                          .Include(p => p.Trainings)
//                                          .FirstOrDefaultAsync(m => m.Id == id);
//             if (planView == null)
//             {
//                 return NotFound();
//             }

//             return View(planView);
//         }

//         // GET: PlanView/Create
//         public IActionResult Create()
//         {
//             ViewBag.Trainings = new MultiSelectList(_context.Trainings, "Id", "Name");
//             return View();
//         }

//         // POST: PlanView/Create
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         // public async Task<IActionResult> Create([Bind("WeekStart,TotalDuration,Comments,TrainingIds")] PlanView planView)
//         // {
//         //     if (ModelState.IsValid)
//         //     {
//         //         // Get the current user ID
//         //         planView.UserId = GetUserId();

//         //         // Retrieve and assign the selected trainings
//         //         if (planView.TrainingIds != null && planView.TrainingIds.Any())
//         //         {
//         //             planView.Trainings = await _context.Trainings
//         //                 .Where(t => planView.TrainingIds.Contains(t.Id))
//         //                 .ToListAsync();
//         //         }

//         //         _context.Add(planView);
//         //         await _context.SaveChangesAsync();
//         //         return RedirectToAction(nameof(Index));
//         //     }
//         //     ViewBag.Trainings = new MultiSelectList(_context.Trainings, "Id", "Name", planView.TrainingIds);
//         //     return View(planView);
//         // }
//         public async Task<IActionResult> Create([Bind("WeekStart,TotalDuration,Comments,TrainingIds")] PlanView planView)
//         {
//             if (ModelState.IsValid)
//             {
//                 // Get the current user ID
//                 planView.UserId = GetUserId();

//                 // Retrieve and assign the selected trainings
//                 if (planView.TrainingIds != null && planView.TrainingIds.Any())
//                 {
//                     planView.Trainings = await _context.Trainings
//                         .Where(t => planView.TrainingIds.Contains(t.Id))
//                         .ToListAsync();
//                 }

//                 _context.Add(planView);
//                 await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewBag.Trainings = new MultiSelectList(_context.Trainings, "Id", "Name", planView.TrainingIds);
//             return View(planView);
//         }

//         // GET: PlanView/Edit/5
//         public async Task<IActionResult> Edit(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var planView = await _context.PlanViews.FindAsync(id);
//             if (planView == null)
//             {
//                 return NotFound();
//             }

//             ViewBag.Trainings = new SelectList(_context.Trainings, "Id", "Name");
//             planView.TrainingIds = planView.Trainings.Select(t => t.Id).ToList();
//             return View(planView);
//         }

//         // POST: PlanView/Edit/5
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(int id, [Bind("Id,WeekStart,TotalDuration,Comments,TrainingIds")] PlanView planView)
//         {
//             if (id != planView.Id)
//             {
//                 return NotFound();
//             }

//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     var existingPlan = await _context.PlanViews
//                                                      .Include(p => p.Trainings)
//                                                      .FirstOrDefaultAsync(p => p.Id == id);

//                     if (existingPlan == null)
//                     {
//                         return NotFound();
//                     }

//                     existingPlan.WeekStart = planView.WeekStart;
//                     existingPlan.TotalDuration = planView.TotalDuration;
//                     existingPlan.Comments = planView.Comments;

//                     existingPlan.Trainings.Clear();
//                     foreach (var trainingId in planView.TrainingIds)
//                     {
//                         var training = await _context.Trainings.FindAsync(trainingId);
//                         if (training != null)
//                         {
//                             existingPlan.Trainings.Add(training);
//                         }
//                     }

//                     _context.Update(existingPlan);
//                     await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!PlanViewExists(planView.Id))
//                     {
//                         return NotFound();
//                     }
//                     else
//                     {
//                         throw;
//                     }
//                 }
//                 return RedirectToAction(nameof(Index));
//             }

//             ViewBag.Trainings = new SelectList(_context.Trainings, "Id", "Name");
//             return View(planView);
//         }

//         // GET: PlanView/Delete/5
//         public async Task<IActionResult> Delete(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var planView = await _context.PlanViews
//                                          .Include(p => p.Trainings)
//                                          .FirstOrDefaultAsync(m => m.Id == id);
//             if (planView == null)
//             {
//                 return NotFound();
//             }

//             return View(planView);
//         }

//         // POST: PlanView/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(int id)
//         {
//             var planView = await _context.PlanViews.FindAsync(id);
//             _context.PlanViews.Remove(planView);
//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }

//         private bool PlanViewExists(int id)
//         {
//             return _context.PlanViews.Any(e => e.Id == id);
//         }

//         private int GetUserId()
//         {
//             var userIdString = HttpContext.Session.GetString("UserId");
//             if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int userId))
//             {
//                 return userId;
//             }
//             throw new Exception("User is not logged in");
//         }
//     }
// }
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
        private readonly ILogger<PlanViewController> _logger;

        public PlanViewController(AppDbContext context, ILogger<PlanViewController> logger)
        {
            _context = context;
            _logger = logger;
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

        // POST: PlanView/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeekStart,TotalDuration,Comments,TrainingIds")] PlanView planView)
        {
            _logger.LogInformation("Starting Create method");

            // Ustawienie UserId przed walidacją modelu
            planView.UserId = GetUserId();
            _logger.LogInformation($"UserId: {planView.UserId}");

            if (ModelState.IsValid)
            {
                _logger.LogInformation("Model state is valid");

                // Logowanie wartości
                _logger.LogInformation($"WeekStart: {planView.WeekStart}");
                _logger.LogInformation($"TotalDuration: {planView.TotalDuration}");
                _logger.LogInformation($"Comments: {planView.Comments}");
                _logger.LogInformation($"TrainingIds: {string.Join(", ", planView.TrainingIds)}");

                // Retrieve and assign the selected trainings
                if (planView.TrainingIds != null && planView.TrainingIds.Any())
                {
                    planView.Trainings = await _context.Trainings
                        .Where(t => planView.TrainingIds.Contains(t.Id))
                        .ToListAsync();
                }

                _context.Add(planView);
                await _context.SaveChangesAsync();
                _logger.LogInformation("PlanView successfully created");
                return RedirectToAction(nameof(Index));
            }

            _logger.LogWarning("Model state is not valid");
            foreach (var state in ModelState)
            {
                if (state.Value.Errors.Any())
                {
                    foreach (var error in state.Value.Errors)
                    {
                        _logger.LogError($"Key: {state.Key}, Error: {error.ErrorMessage}");
                    }
                }
            }

            ViewBag.Trainings = new MultiSelectList(_context.Trainings, "Id", "Name", planView.TrainingIds);
            return View(planView);
        }

        // Other actions...

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
    }
}
