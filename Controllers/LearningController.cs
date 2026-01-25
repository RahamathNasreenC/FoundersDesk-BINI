using Microsoft.AspNetCore.Mvc;
using FoundersDesk.Data;
using Microsoft.EntityFrameworkCore;

namespace FoundersDesk.Controllers
{
    public class LearningController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LearningController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Course(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Modules)
                    .ThenInclude(m => m.Videos)
                .FirstOrDefaultAsync(c => c.CourseId == id);   // ✅ FIX

            if (course == null)
                return NotFound();

            return View(course);
        }

    }
}
