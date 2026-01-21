using FoundersDesk.Data;
using FoundersDesk.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace FoundersDesk.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProfileController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Complete()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Auth");

            var user = _db.Users.Find(userId.Value);

            var vm = new CompleteProfileViewModel
            {
                Email = user.Email,
                FullName = user.FullName, // Pre-fill if they are editing
                PhoneNumber = user.PhoneNumber

            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete(CompleteProfileViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Auth");

            if (!ModelState.IsValid)
                return View(model);

            var user = _db.Users.Find(userId.Value);

            // Update basic info
            user.FullName = model.FullName;
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;
            


            // ✅ STANDARD AVATAR LOGIC
            // Instead of local files, we use a reliable API that generates a circle avatar based on their name.
            // This ensures every user has a clean, properly placed image immediately.


            user.IsProfileCompleted = true;
            await _db.SaveChangesAsync();

            // Redirect based on role
            if (user.Role == Models.UserRole.Staff)
                return RedirectToAction("Dashboard", "Staff");
            else
                return RedirectToAction("Dashboard", "Intern");
        }
    }
}