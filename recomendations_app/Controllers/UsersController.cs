using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recomendations_app.CloudStorage;
using Recomendations_app.Data;
using Recomendations_app.Models;

namespace Recomendations_app.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICloudStorage _cloudStorage;

        public UsersController(ApplicationDbContext context, ICloudStorage cloudStorage)
        {
            _context = context;
            _cloudStorage = cloudStorage;
        }

        // GET: Users
        public async Task<IActionResult> Index(int? rate,string name, string sortOrder)
        {
            ViewBag.RateSortParm = String.IsNullOrEmpty(sortOrder) ? "rate_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var user = await _context.Users
                .Include(u => u.Reviews)
                .Include(u => u.Reviews)
                .FirstOrDefaultAsync(u => u.UserName == name);
            var reviews = await _context.Reviews
                .Include(r => r.Comments)
                .Include(r => r.Tags)
                .Include(r => r.Likes)
                .Include(r => r.Images)
                .Where(x => x.Author.UserName == name).ToListAsync();
           user.Reviews = reviews;
            switch (sortOrder)
            {
                case "name_desc":
                    user.Reviews = user.Reviews.OrderByDescending(s => s.AuthorGrade).ToList();
                    break;
                case "Date":
                    user.Reviews = user.Reviews.OrderBy(s => s.DateOfCreationInUTC).ToList();
                    break;
                case "date_desc":
                    user.Reviews = user.Reviews.OrderByDescending(s => s.DateOfCreationInUTC).ToList();
                    break;
                default:
                    user.Reviews = user.Reviews.OrderBy(s => s.AuthorGrade).ToList();
                    break;
            }
            if (rate != null)
            {
                user.Reviews = user.Reviews.Where(r => r.AuthorGrade >= rate).ToList();
            }
            return View(user);
        }
    }
}
