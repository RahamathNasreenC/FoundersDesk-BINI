using FoundersDesk.Data;
using FoundersDesk.DTOs;
using FoundersDesk.Interfaces;
using FoundersDesk.Models;
using FoundersDesk.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace FoundersDesk.Controllers
{
    public class StaffController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly IAuthRepository _authRepository;
        private readonly ApplicationDbContext _context;
        public StaffController(
     IUserRepository userRepository,
     IVideoRepository videoRepository,
     IAuthRepository authRepository,
     ApplicationDbContext context)
        {
            _userRepository = userRepository;
            _videoRepository = videoRepository;
            _authRepository = authRepository;
            _context = context;
        }

        // GET: /Staff /Dashboard
        public async Task<IActionResult> Dashboard()
        {
            // Check authentication
            var sessionToken = HttpContext.Session.GetString("SessionToken");
            if (string.IsNullOrEmpty(sessionToken) || !await _authRepository.ValidateSessionAsync(sessionToken))
            {
                return RedirectToAction("Login", "Auth", new { role = "staff" });
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth", new { role = "staff" });
            }

            // Get user details
            var user = await _userRepository.GetUserByIdAsync(userId.Value);
            if (user == null || user.Role != Models.UserRole.Staff)
            {
                return RedirectToAction("Login", "Auth", new { role = "staff" });
            }
            // ========== RESOURCES LOGIC (same as Intern) ==========

            // Get documents
            var documents = _context.Documents.ToList();

            // Get acknowledgements
            var acknowledgements = _context.ResourceAcknowledgements
                .Where(r => r.Username == user.Username)
                .ToList();

            // Check which are still valid (not outdated)
            var validAcknowledgements = acknowledgements
                .Join(documents,
                    ack => ack.ResourceType,
                    doc => doc.DocumentType,
                    (ack, doc) => new
                    {
                        ack.ResourceType,
                        IsValid = ack.AcknowledgedAt >= doc.UpdatedAt
                    })
                .Where(x => x.IsValid)
                .Select(x => x.ResourceType)
                .ToList();

            // Get digital signature
            var signature = _context.DigitalSignatures
                .FirstOrDefault(s => s.Username == user.Username);

            // Get staff videos
            var courses = _context.Courses
    .Include(c => c.Modules)
        .ThenInclude(m => m.Videos)
    .Where(c =>
        c.IsActive &&
        c.RoleType == user.Role.ToString() &&
        (string.IsNullOrEmpty(c.JobRole) || c.JobRole == user.JobRole)
    )
    .OrderBy(c => c.DisplayOrder)
    .ToList();




            var viewModel = new StaffDashboardViewModel
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

                // ✅ THIS IS THE MISSING PART
                AcknowledgedResources = validAcknowledgements,
                IsSignatureUploaded = signature != null,
                SignatureUploadedAt = signature?.UploadedAt,

                Courses = courses.Select(c => new CourseViewModel
                {
                    CourseId = c.CourseId,
                    Title = c.Title,
                    Description = c.Description,
                    Modules = c.Modules
        .OrderBy(m => m.DisplayOrder)
        .Select(m => new ModuleViewModel
        {
            ModuleId = m.ModuleId,
            Title = m.Title,
            Videos = m.Videos
                .OrderBy(v => v.DisplayOrder)
                .Select(v => new VideoDto
                {
                    VideoId = v.VideoId,
                    Title = v.Title,
                    Description = v.Description,
                    Category = v.Category,
                    RoleType = v.RoleType,
                    VideoUrl = v.VideoUrl,
                    Icon = v.Icon
                }).ToList()
        }).ToList()
                }).ToList()

            };


            return View(viewModel);
        }
    }
}