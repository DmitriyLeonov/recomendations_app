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
        public async Task<IActionResult> Index(string name)
        {
            var reviews = _context.Reviews
                .Include(r => r.Comments)
                .Include(r => r.Tags)
                .Include(r => r.Likes)
                .Include(r => r.Images).Where(x => x.AuthorName == name);
            return View(await reviews.ToListAsync());
        }
    }
}
