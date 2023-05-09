using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly ICloudStorage _cloudStorage;

        public ReviewController(ApplicationDbContext context, ICloudStorage cloudStorage)
        {
            _context = context;
            _cloudStorage = cloudStorage;
        }

        public ActionResult Index(string query)
        {
            var result = new List<ReviewModel>();
            if (!query.IsNullOrEmpty())
            {
                query = query.Trim();
                query = query.Replace(" ", " <-> ");
                result = _context.Reviews.Where(x =>
                    x.SearchVector.Matches(EF.Functions.ToTsQuery($"{query}:*"))
                ).Include(r => r.Tags).ToList();
            }
            return View(result);
        }

        // GET: Review/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Reviews == null)
            {
                return NotFound();
            }

            var reviewModel = await _context.Reviews
                //.Include(r => r.Author)
                //.Include(r => r.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reviewModel == null)
            {
                return NotFound();
            }
            return View(reviewModel);
        }

        // GET: Review/Create
        public IActionResult Create()
        {
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");
            return View();
        }

        // POST: Review/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReviewCategory,AuthorGrade,ReviewBody,DateOfCreationInUTC,ImageStorageName,ImageLink,AuthorName,ImageFile")] ReviewModel reviewModel, string tags)
        {
            var tagList = await AddTagToDb(tags);
            reviewModel.Id = Guid.NewGuid().ToString();
            reviewModel.AuthorName = this.User.Identity.Name;
            reviewModel.DateOfCreationInUTC = DateTime.UtcNow;
            if (!ModelState.IsValid)
            {
                if (reviewModel.ImageFile != null)
                {
                    await UploadFile(reviewModel);
                }
                _context.Reviews.Add(reviewModel);
                reviewModel.Tags.AddRange(tagList);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            //ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", reviewModel.SubjectId);
            return View(reviewModel);
        }

        // GET: Review/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Reviews == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews.Include(r => r.Tags).ToListAsync();
            var reviewModel = reviews.Where(r => r.Id == id).FirstOrDefault();
            if (reviewModel == null)
            {
                return NotFound();
            }
            ViewData["AuthorName"] = new SelectList(_context.Set<UserModel>(), "Id", "Id", reviewModel.AuthorName);
            //ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", reviewModel.SubjectId);
            return View(reviewModel);
        }

        // POST: Review/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,ReviewCategory,AuthorGrade,ReviewBody,DateOfCreationInUTC,SubjectId,ImageStorageName,ImageLink,ImageFile,AuthorName")] ReviewModel reviewModel, string tags)
        {
            if (id != reviewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (reviewModel.ImageFile != null)
                    {
                        if (reviewModel.ImageStorageName != null)
                        {
                            await _cloudStorage.DeleteFileAsync(reviewModel.ImageStorageName);
                        }

                        await UploadFile(reviewModel);
                    }
                    _context.Reviews.Update(reviewModel);
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
                return RedirectToAction("Index","Home");
            }
            ViewData["AuthorName"] = new SelectList(_context.Set<UserModel>(), "Id", "Id", reviewModel.AuthorName);
            //ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", reviewModel.SubjectId);
            return View(reviewModel);
        }

        // GET: Review/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Reviews == null)
            {
                return NotFound();
            }

            var reviewModel = await _context.Reviews
                //.Include(r => r.Author)
                //.Include(r => r.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reviewModel == null)
            {
                return NotFound();
            }

            return View(reviewModel);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Reviews == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reviews'  is null.");
            }
            var reviews = await _context.Reviews.Include(r => r.Tags).ToListAsync();
            var reviewModel = reviews.Where(r => r.Id == id).FirstOrDefault();
            if (reviewModel != null)
            {
                if (reviewModel.ImageStorageName != null)
                {
                    await _cloudStorage.DeleteFileAsync(reviewModel.ImageStorageName);
                }

                _context.Tags.RemoveRange(reviewModel.Tags);
                _context.Reviews.Remove(reviewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool ReviewModelExists(string id)
        {
          return (_context.Reviews?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task UploadFile(ReviewModel reviewModel)
        {
            string fileNameForStorage = FormFileName(reviewModel.Title, reviewModel.ImageFile.FileName);
            reviewModel.ImageLink = await _cloudStorage.UploadFileAsync(reviewModel.ImageFile, fileNameForStorage);
            reviewModel.ImageStorageName = fileNameForStorage;
        }

        private static string FormFileName(string title, string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            var fileNameForStorage = $"{title}-{DateTime.Now.ToString("yyyyMMddHHmmss")}{fileExtension}";
            return fileNameForStorage;
        }

        private async Task<List<TagModel>> AddTagToDb(string tags)
        {
            List<TagModel> tagList = new List<TagModel>();
            bool isTagExists = false;
            foreach (var tag in tags.Split(","))
            {
                tagList.Add(new TagModel(tag.ToString()));
            }
            _context.Tags.AddRange(tagList);
            return tagList;
        }
    }
}
