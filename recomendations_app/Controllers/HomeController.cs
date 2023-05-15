using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recomendations_app.Data;
using Recomendations_app.Models;
using System.Diagnostics;

namespace Recomendations_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.RateSortParm = String.IsNullOrEmpty(sortOrder) ? "rate_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var reviews = await _context.Reviews
                .Include(r => r.Comments)
                .Include(r => r.Tags)
                .Include(r => r.Likes)
                .Include(r => r.Images).ToListAsync();
            foreach (var review in reviews)
            {
                review.Author = await _context.Users.FirstOrDefaultAsync(u => u.Id == review.AuthorId);
            }
            switch (sortOrder)
            { 
                case "rate_desc":
                    reviews = reviews.OrderByDescending(s => s.AuthorGrade).ToList();
                    break;
                case "Date":
                    reviews = reviews.OrderBy(s => s.DateOfCreationInUTC).ToList();
                    break;
                case "date_desc":
                    reviews = reviews.OrderByDescending(s => s.DateOfCreationInUTC).ToList();
                    break;
                default:
                    reviews = reviews.OrderBy(s => s.AuthorGrade).ToList();
                    break;
        }
            return View(reviews);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}