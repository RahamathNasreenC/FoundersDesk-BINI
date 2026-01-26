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
        private readonly IVideoRepository _videoRepository;
        private readonly IAuthRepository _authRepository;
        private readonly ApplicationDbContext _context;


        public InternController(IUserRepository userRepository, IVideoRepository videoRepository, IAuthRepository authRepository, ApplicationDbContext context)
        {
            _userRepository = userRepository;
            _videoRepository = videoRepository;
            _authRepository = authRepository;
            _context = context;

        }

        // GET: /Intern/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            // Check authentication
            var sessionToken = HttpContext.Session.GetString("SessionToken");
            if (string.IsNullOrEmpty(sessionToken) || !await _authRepository.ValidateSessionAsync(sessionToken))
            {
                return RedirectToAction("Login", "Auth", new { role = "intern" });
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth", new { role = "intern" });
            }

            // Get user details
            var user = await _userRepository.GetUserByIdAsync(userId.Value);
            if (user == null || user.Role != Models.UserRole.Intern)
            {
                return RedirectToAction("Login", "Auth", new { role = "intern" });
            }
            // Get already acknowledged resources for this user
            // ========== NEW DOCUMENT ACK LOGIC ==========

            var documents = _context.Documents
                .Where(d => d.RoleType == user.Role.ToString() && d.IsActive)
                .ToList();

            var acknowledgements = _context.ResourceAcknowledgements
                .Where(a => a.Username == user.Username)
                .ToList();

            // Only acknowledgements that are still valid (not outdated)
            var validAcknowledgedDocIds = acknowledgements
                .Join(documents,
                    ack => ack.DocumentId,
                    doc => doc.DocumentId,
                    (ack, doc) => new
                    {
                        doc.DocumentType,
                        IsValid = ack.AcknowledgedAt >= doc.UpdatedAt
                    })
                .Where(x => x.IsValid)
                .Select(x => x.DocumentType)
                .ToList();


            // Get digital signature status
            var signature = _context.DigitalSignatures
                .FirstOrDefault(s => s.Username == user.Username);



            // Get videos
            var orientationVideos = await _videoRepository.GetVideosByRoleAndCategoryAsync("general", "orientation");
            var projectVideos = await _videoRepository.GetVideosByRoleAndCategoryAsync("general", "projects");
            var cultureVideos = await _videoRepository.GetVideosByRoleAndCategoryAsync("general", "culture");
            var technicalVideos = await _videoRepository.GetVideosByRoleAsync("technical");
            var nonTechnicalVideos = await _videoRepository.GetVideosByRoleAsync("non-technical");

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
                AcknowledgedResources = validAcknowledgedDocIds,

                IsSignatureUploaded = signature != null,
                SignatureUploadedAt = signature?.UploadedAt,


                OrientationVideos = orientationVideos.Select(v => new VideoDto
                {
                    VideoId = v.VideoId,
                    Title = v.Title,
                    Description = v.Description,
                    Category = v.Category,
                    RoleType = v.RoleType,
                    VideoUrl = v.VideoUrl ?? string.Empty,

                    Icon = v.Icon
                }).ToList(),
                ProjectVideos = projectVideos.Select(v => new VideoDto
                {
                    VideoId = v.VideoId,
                    Title = v.Title,
                    Description = v.Description,
                    Category = v.Category,
                    RoleType = v.RoleType,
                    VideoUrl = v.VideoUrl ?? string.Empty,

                    Icon = v.Icon
                }).ToList(),
                CultureVideos = cultureVideos.Select(v => new VideoDto
                {
                    VideoId = v.VideoId,
                    Title = v.Title,
                    Description = v.Description,
                    Category = v.Category,
                    RoleType = v.RoleType,
                    VideoUrl = v.VideoUrl ?? string.Empty,

                    Icon = v.Icon
                }).ToList(),
                TechnicalVideos = technicalVideos.Select(v => new VideoDto
                {
                    VideoId = v.VideoId,
                    Title = v.Title,
                    Description = v.Description,
                    Category = v.Category,
                    RoleType = v.RoleType,
                    VideoUrl = v.VideoUrl ?? string.Empty,

                    Icon = v.Icon
                }).ToList(),
                NonTechnicalVideos = nonTechnicalVideos.Select(v => new VideoDto
                {
                    VideoId = v.VideoId,
                    Title = v.Title,
                    Description = v.Description,
                    Category = v.Category,
                    RoleType = v.RoleType,
                    VideoUrl = v.VideoUrl ?? string.Empty,

                    Icon = v.Icon
                }).ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]   // 👈 ADD THIS LINE
        public async Task<IActionResult> UploadSignature(IFormFile signature)

        {
            if (signature == null || signature.Length == 0)
                return Json(new { success = false, message = "No file uploaded" });

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return Json(new { success = false, message = "Session expired" });

            var user = await _userRepository.GetUserByIdAsync(userId.Value);
            if (user == null)
                return Json(new { success = false, message = "User not found" });

            // Save file
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "signatures");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{user.Username}_{DateTime.UtcNow.Ticks}{Path.GetExtension(signature.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await signature.CopyToAsync(stream);
            }

            // Save DB record
            var digitalSignature = new DigitalSignature
            {
                Username = user.Username,
                FilePath = "/signatures/" + fileName,
                UploadedAt = DateTime.UtcNow
            };

            _context.DigitalSignatures.Add(digitalSignature);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }


    }

}
