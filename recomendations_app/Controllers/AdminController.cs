using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recomendations_app.Data;
using Recomendations_app.Models;

namespace Recomendations_app.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserModel> _userManager;
        public AdminController( ApplicationDbContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> DeleteAsync(string selectedUser)
        {
            UserModel user = new UserModel();
            string redirectUrl = "";
            var currentUser = HttpContext.User.Identity.Name;
            user = _context.Users
                .Include(r => r.Reviews)
                .FirstOrDefault(u => u.UserName == selectedUser);
            user.Reviews = await _context.Reviews.Include(r => r.Comments)
                .Include(r => r.Tags)
                .Include(r => r.Likes)
                .Include(r => r.Images)
                .Where(r => r.AuthorId == user.Id).ToListAsync();
            if (!await _userManager.IsInRoleAsync(user, "Administrator"))
            {
                if (user.UserName != currentUser)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    redirectUrl = "~/Admin";
                }
                else
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    redirectUrl = "~/Identity/Account/Login";
                }

                foreach (var review in user.Reviews)
                {
                    _context.Images.RemoveRange(review.Images);
                    _context.Comments.RemoveRange(review.Comments);
                    _context.Tags.RemoveRange(review.Tags);
                    _context.Likes.RemoveRange(review.Likes);
                }
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            
            return Redirect(redirectUrl);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> BlockAsync(string selectedUser)
        {
            UserModel user = new UserModel();
            string redirectUrl = "";
            var currentUser = HttpContext.User.Identity.Name;
            user = _context.Users.FirstOrDefault(u => u.UserName == selectedUser);
            if (!await _userManager.IsInRoleAsync(user, "Administrator"))
            {
                if (user.UserName != currentUser)
                {
                    user.LockoutEnd = DateTimeOffset.MaxValue;
                    await _userManager.UpdateSecurityStampAsync(user);
                    redirectUrl = "~/Admin";
                }
                else
                {
                    user.LockoutEnd = DateTimeOffset.MaxValue;
                    await _userManager.UpdateSecurityStampAsync(user);
                    redirectUrl = "~/Identity/Account/Login";
                }
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
            return Redirect(redirectUrl);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> MakeAdminAsync(string selectedUser)
        {
            UserModel user = new UserModel();
            user = _context.Users.FirstOrDefault(u => u.UserName == selectedUser);
            var redirectUrl = "~/Admin";
            await _userManager.AddToRoleAsync(user, "Administrator");
            await _context.SaveChangesAsync();
            return Redirect(redirectUrl);
        }
    }
}
