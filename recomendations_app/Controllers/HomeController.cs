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

        public async Task<IActionResult> Index()
        {
            var reviews = _context.Reviews.Include(r => r.Tags)/*.Include(r => r.Subject)*/;
            return View(await reviews.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}