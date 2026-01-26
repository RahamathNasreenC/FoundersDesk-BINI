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
using System.IO;
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
            // ========== NEW DOCUMENT ACK LOGIC ==========

            var documents = _context.Documents
                .Where(d => d.RoleType == user.Role.ToString() && d.IsActive)
                .ToList();

            var policyItems = documents.Select(doc =>
            {
                var ack = _context.ResourceAcknowledgements
                    .FirstOrDefault(a =>
                        a.Username == user.Username &&
                        a.DocumentId == doc.DocumentId);

                return new PolicyDocumentItem
                {
                    DocumentId = doc.DocumentId,
                    Title = doc.Title,
                    FilePath = doc.FilePath,
                    RequiresSignature = doc.RequiresSignature,
                    IsAcknowledged = ack != null && ack.AcknowledgedAt >= doc.UpdatedAt,
                    AcknowledgedAt = ack?.AcknowledgedAt
                };
            }).ToList();

            var companyPolicies = new CompanyPolicyViewModel
            {
                Documents = policyItems
            };



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



            var trainingResources = _context.TrainingResources
    .Where(tr =>
        tr.IsActive &&
        tr.RoleType == user.Role.ToString() &&
        (
            tr.IsGeneric ||
            tr.JobRole == user.JobRole
        )
    )
    .OrderBy(tr => tr.DisplayOrder)
    .Select(tr => new TrainingResourceViewModel
    {
        TrainingResourceId = tr.TrainingResourceId,
        Title = tr.Title,
        Description = tr.Description,
        PageKey = tr.PageKey   // ✅ opens training page
    })
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

                CompanyPolicies = companyPolicies,

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
                }).ToList(),

                // ✅ THIS WAS MISSING
                TrainingResources = trainingResources
            };

            return View(viewModel);


        }
        [HttpPost]
        public async Task<IActionResult> UploadSignature(IFormFile signature)
        {
            try
            {
                var username = HttpContext.Session.GetString("Username");

                if (string.IsNullOrEmpty(username))
                    return Json(new { success = false, message = "Session expired" });

                if (signature == null || signature.Length == 0)
                    return Json(new { success = false, message = "No file uploaded" });

                // Create folder if not exists
                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "signatures");
                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                // Save file
                var fileName = username + "_" + Guid.NewGuid() + Path.GetExtension(signature.FileName);
                var filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await signature.CopyToAsync(stream);
                }

                var dbPath = "/uploads/signatures/" + fileName;

                // Check if signature already exists
                var existing = _context.DigitalSignatures.FirstOrDefault(s => s.Username == username);

                if (existing == null)
                {
                    var sig = new DigitalSignature
                    {
                        Username = username,
                        FilePath = dbPath,
                        UploadedAt = DateTime.UtcNow
                    };
                    _context.DigitalSignatures.Add(sig);
                }
                else
                {
                    existing.FilePath = dbPath;
                    existing.UploadedAt = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}