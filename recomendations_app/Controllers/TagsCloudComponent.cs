using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recomendations_app.Data;

namespace Recomendations_app.Controllers
{
    public class TagsCloudComponent : ViewComponent
    {
        // GET: TagsCloudComponent
        private readonly ApplicationDbContext _context;

        public TagsCloudComponent(ApplicationDbContext context)
        {
            _context = context;
        }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var result = await _context.Tags.GroupBy(p => p.Name)
            .Select(g => g.First())
            .ToListAsync();
            return View(result);
        }

    }
}
