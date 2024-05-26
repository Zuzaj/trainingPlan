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

            if (ModelState.IsValid)
            {
                _logger.LogInformation("Model state is valid");
                // Get the current user ID
                planView.UserId = GetUserId();
                _logger.LogInformation($"UserId: {planView.UserId}");

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
