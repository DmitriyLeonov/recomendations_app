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
                    x.SearchVector.Matches(EF.Functions.ToTsQuery($"{query}:*")))
                    .Include(r => r.Comments)
                    .Include(r => r.Tags)
                    .Include(r => r.Likes)
                    .Include(r => r.Images).ToList();
                foreach (var review in result)
                {
                    review.Author = _context.Users.FirstOrDefault(x => x.Id == review.AuthorId);
                }
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
                .Include(r => r.Comments)
                .Include(r => r.Tags)
                .Include(r => r.Likes)
                .Include(r => r.Images)
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
            return View();
        }

        // POST: Review/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReviewCategory,Subject,AuthorGrade,ReviewBody,DateOfCreationInUTC,AuthorName,Images")] ReviewModel reviewModel, string tags, IFormFile[] images)
        {
            var tagList = await AddTagToDb(tags);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == this.User.Identity.Name);
            reviewModel.Id = Guid.NewGuid().ToString();
            reviewModel.Author = user;
            reviewModel.AuthorId = user.Id;
            reviewModel.DateOfCreationInUTC = DateTime.UtcNow;
            if (!ModelState.IsValid)
            {
                if (images.Length >= 1)
                {
                    await UploadFile(images, reviewModel);
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
            return View(reviewModel);
        }

        // POST: Review/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,ReviewCategory,AuthorGrade,ReviewBody,DateOfCreationInUTC,Subject,ImageStorageName,ImageLink,ImageFile,AuthorName")] ReviewModel reviewModel)
        {
            if (id != reviewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            var reviewModel = await _context.Reviews
                .Include(r => r.Comments)
                .Include(r => r.Tags)
                .Include(r => r.Likes)
                .Include(r => r.Images).FirstOrDefaultAsync(x => x.Id == id);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == reviewModel.AuthorId);
            user.LikesCount -= reviewModel.Likes.Count;
            if (reviewModel != null)
            {
                if (reviewModel.Images != null)
                {
                    foreach (var image in reviewModel.Images)
                    {
                        await _cloudStorage.DeleteFileAsync(image.ImageStorageName);
                    }
                }
                _context.Tags.RemoveRange(reviewModel.Tags);
                _context.Likes.RemoveRange(reviewModel.Likes);
                _context.Comments.RemoveRange(reviewModel.Comments);
                _context.Images.RemoveRange(reviewModel.Images);
                _context.Reviews.Remove(reviewModel);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AddComment(string commentText,string id)
        {
            if (commentText != null)
            {
                var comment = new Comment()
                {
                    AuthorName = this.User.Identity.Name,
                    CommentBody = commentText,
                    DateOfCreationInUTC = DateTime.UtcNow,
                    Review = await _context.Reviews.FirstOrDefaultAsync(m => m.Id == id),
                    ReviewId = id
                };
                await _context.Comments.AddAsync(comment);
                await _context.SaveChangesAsync();
            }
            return Redirect("/Review/Details/" + id);
        }
        public async Task<IActionResult> AddLike(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == this.User.Identity.Name);
            var rewiew = await _context.Reviews.FirstOrDefaultAsync(m => m.Id == id);
            var toUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == rewiew.Author.UserName);
            var like = new LikeModel()
            {
                FromUser = user,
                FromUserId = user.Id,
                Review = rewiew,
                ReviewId = id,
                ToUser = toUser,
                ToUserId = toUser.Id

            };
            toUser.LikesCount++;
            await _context.AddAsync(like);
            await _context.SaveChangesAsync();
            return Redirect("/Review/Details/" + id);
        }

        private bool ReviewModelExists(string id)
        {
          return (_context.Reviews?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task UploadFile(IFormFile[] images,ReviewModel reviewModel)
        {
           foreach (var image in images)
           {
               string fileNameForStorage = FormFileName(reviewModel.Title, image.FileName);
                ImageModel imageModel = new ImageModel()
               {
                   ImageFile = image,
                   ImageLink = await _cloudStorage.UploadFileAsync(image, fileNameForStorage),
                   ImageStorageName = fileNameForStorage,
                   Review = reviewModel,
                   ReviewId = reviewModel.Id
               };
                await _context.Images.AddAsync(imageModel);
           }
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
            foreach (var tag in tags.Split(","))
            {
                tagList.Add(new TagModel(tag.ToString()));
            }
            _context.Tags.AddRange(tagList);
            return tagList;
        }
    }
}
