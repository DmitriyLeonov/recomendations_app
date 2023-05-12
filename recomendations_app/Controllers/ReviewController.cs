using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Recomendations_app.CloudStorage;
using Recomendations_app.Data;
using Recomendations_app.Models;

namespace Recomendations_app.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;
        

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public ActionResult Index(string query)
        {
            var result = new List<ReviewModel>();
            if (!query.IsNullOrEmpty())
            {
                query = query.Trim();
                query = query.Replace(" ", " <-> ");
                result = _context.Reviews.Where(x =>
                    x.SearchVector.Matches(EF.Functions.ToTsQuery($"{query}:*")))
                    .Include(r => r.Comments)
                    .Include(r => r.Tags)
                    .Include(r => r.Likes)
                    .Include(r => r.Images).ToList();
            }
            return View(result);
        }

    }
}
