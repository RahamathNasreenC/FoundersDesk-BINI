using FoundersDesk.Data;
using FoundersDesk.DTOs;
using FoundersDesk.Interfaces;
using FoundersDesk.Models;
using FoundersDesk.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FoundersDesk.Controllers
{
    public class InternController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthRepository _authRepository;
        private readonly ApplicationDbContext _context;

        public InternController(
            IUserRepository userRepository,
            IAuthRepository authRepository,
            ApplicationDbContext context)
        {
            _userRepository = userRepository;
            _authRepository = authRepository;
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            // 🔐 Auth check
            var sessionToken = HttpContext.Session.GetString("SessionToken");
            if (string.IsNullOrEmpty(sessionToken) || !await _authRepository.ValidateSessionAsync(sessionToken))
                return RedirectToAction("Login", "Auth", new { role = "intern" });

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Auth", new { role = "intern" });

            var user = await _userRepository.GetUserByIdAsync(userId.Value);
            if (user == null || user.Role != UserRole.Intern)
                return RedirectToAction("Login", "Auth", new { role = "intern" });

            // 📄 Documents
            var documents = _context.Documents
                .Where(d => d.RoleType == user.Role.ToString() && d.IsActive)
                .ToList();

            var acknowledgements = _context.ResourceAcknowledgements
                .Where(a => a.Username == user.Username)
                .ToList();

            var acknowledgedDocTypes = acknowledgements
                .Join(documents,
                    ack => ack.DocumentId,
                    doc => doc.DocumentId,
                    (ack, doc) => new { doc.DocumentType, IsValid = ack.AcknowledgedAt >= doc.UpdatedAt })
                .Where(x => x.IsValid)
                .Select(x => x.DocumentType)
                .ToList();

            // ✍️ Signature
            var signature = _context.DigitalSignatures
                .FirstOrDefault(s => s.Username == user.Username);

            // 🎥 DASHBOARD VIDEOS (FIXED)
            var videos = _context.Videos
                .Where(v =>
                    v.IsActive &&
                    v.RoleType == user.Role.ToString() &&
                    (string.IsNullOrEmpty(v.JobRole) || v.JobRole == user.JobRole))
                .OrderBy(v => v.DisplayOrder)
                .ToList();

            var viewModel = new InternDashboardViewModel
            {
                User = new UserDto
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Email = user.Email,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    JobRole = user.JobRole,
                    IsProfileCompleted = user.IsProfileCompleted,
                    Role = user.Role.ToString()
                },

                AcknowledgedResources = acknowledgedDocTypes,
                IsSignatureUploaded = signature != null,
                SignatureUploadedAt = signature?.UploadedAt,

                OrientationVideos = videos
                    .Where(v => v.Category == "orientation")
                    .Select(ToVideoDto)
                    .ToList(),

                ProjectVideos = videos
                    .Where(v => v.Category == "projects")
                    .Select(ToVideoDto)
                    .ToList(),

                CultureVideos = videos
                    .Where(v => v.Category == "culture")
                    .Select(ToVideoDto)
                    .ToList(),

                TechnicalVideos = videos
                    .Where(v => v.Category == "technical")
                    .Select(ToVideoDto)
                    .ToList(),

                NonTechnicalVideos = videos
                    .Where(v => v.Category == "non-technical")
                    .Select(ToVideoDto)
                    .ToList()
            };

            return View(viewModel);
        }

        private static VideoDto ToVideoDto(Video v)
        {
            return new VideoDto
            {
                VideoId = v.VideoId,
                Title = v.Title,
                Description = v.Description,
                Category = v.Category,
                RoleType = v.RoleType,
                VideoUrl = v.VideoUrl ?? "",
                Icon = v.Icon
            };
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> UploadSignature(IFormFile signature)
        {
            if (signature == null || signature.Length == 0)
                return Json(new { success = false });

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return Json(new { success = false });

            var user = await _userRepository.GetUserByIdAsync(userId.Value);
            if (user == null)
                return Json(new { success = false });

            var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "signatures");
            Directory.CreateDirectory(folder);

            var fileName = $"{user.Username}_{DateTime.UtcNow.Ticks}{Path.GetExtension(signature.FileName)}";
            var path = Path.Combine(folder, fileName);

            using var stream = new FileStream(path, FileMode.Create);
            await signature.CopyToAsync(stream);

            _context.DigitalSignatures.Add(new DigitalSignature
            {
                Username = user.Username,
                FilePath = "/signatures/" + fileName,
                UploadedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}
