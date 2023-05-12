using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recomendations_app.Data;
using Recomendations_app.Models;

namespace Recomendations_app.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.Reviews != null ? 
                          View(await _context.Reviews.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Reviews'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Reviews == null)
            {
                return NotFound();
            }

            var reviewModel = await _context.Reviews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reviewModel == null)
            {
                return NotFound();
            }

            return View(reviewModel);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReviewCategory,AuthorGrade,ReviewBody,DateOfCreationInUTC,AuthorName,SearchVector")] ReviewModel reviewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reviewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reviewModel);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Reviews == null)
            {
                return NotFound();
            }

            var reviewModel = await _context.Reviews.FindAsync(id);
            if (reviewModel == null)
            {
                return NotFound();
            }
            return View(reviewModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,ReviewCategory,AuthorGrade,ReviewBody,DateOfCreationInUTC,AuthorName,SearchVector")] ReviewModel reviewModel)
        {
            if (id != reviewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reviewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewModelExists(reviewModel.Id))
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
            return View(reviewModel);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Reviews == null)
            {
                return NotFound();
            }

            var reviewModel = await _context.Reviews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reviewModel == null)
            {
                return NotFound();
            }

            return View(reviewModel);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Reviews == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reviews'  is null.");
            }
            var reviewModel = await _context.Reviews.FindAsync(id);
            if (reviewModel != null)
            {
                _context.Reviews.Remove(reviewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewModelExists(string id)
        {
          return (_context.Reviews?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
