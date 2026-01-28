using FoundersDesk.Interfaces;
using FoundersDesk.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FoundersDesk.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Complete()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            var user = await _profileRepository.GetUserByIdAsync(userId.Value);
            if (user == null)
                return RedirectToAction("Login", "Auth");

            var vm = new CompleteProfileViewModel
            {
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete(CompleteProfileViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            if (!ModelState.IsValid)
                return View(model);

            var user = await _profileRepository.GetUserByIdAsync(userId.Value);
            if (user == null)
                return RedirectToAction("Login", "Auth");

            // Update profile
            user.FullName = model.FullName;
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;
            user.IsProfileCompleted = true;

            await _profileRepository.UpdateProfileAsync(user);

            // Redirect by role
            return user.Role == Models.UserRole.Staff
                ? RedirectToAction("Dashboard", "Staff")
                : RedirectToAction("Dashboard", "Intern");
        }
    }
}
